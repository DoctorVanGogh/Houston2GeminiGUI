using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Houston2DaiGUI
{
    public class FormConverter
    {
        private int _indent;
        private readonly XElement _xml;

        public bool UseSpacesForTabs { get; set; }
        public int IndentSize { get; set; }
        public StringBuilder Output { get; set; }
        public bool TranslateAnchorPoints { get; set; }

        private string IndentString
        {
            get
            {
                return UseSpacesForTabs 
                           ? new string(' ', _indent*IndentSize)
                           : new string('\t', _indent);
            }
        }

        public FormConverter(XElement xml)
        {
            UseSpacesForTabs = false;
            TranslateAnchorPoints = false;
            IndentSize = 2;
            Output = new StringBuilder();
            _xml = xml;
        }


        private string GetName()
        {
            var strFormName = _xml.Attributes().FirstOrDefault(x => x.Name == "Name");
            return strFormName != null ? strFormName.Value : "Wnd";
        }

        public string Parse()
        {
            _indent = 0;
            Output.Append("local t" + GetName() + "Def = ");
            ProcessWindowNode(_xml);
            return Output.ToString();
        }

        private static bool AttributeHasDefaultValue(XAttribute attr)
        {
            return (AttributeDefaults.ContainsKey(attr.Name.ToString()) && AttributeDefaults[attr.Name.ToString()] == attr.Value);
        }

        private void ProcessWindowNode(XElement wndNode)
        {
            var attrs = wndNode.Attributes().ToArray();
            Output.AppendLine(IndentString + "{");
            _indent++;

            // parse the AnchorOffset and AnchorPoint attributes
            foreach (var anchorType in new[] {"AnchorOffset", "AnchorPoint"})
            {
                if (!attrs.Any(x => x.Name.ToString().EndsWith(anchorType))) continue;
                var anchor = GetAnchorTable(attrs, anchorType);
                if (anchor == null) continue;
                Output.AppendFormat("{0}{1} = {2},\n", IndentString, anchorType + "s", anchor);
            }

            // parse the element's attributes
            foreach (var attr in attrs.Where(x => !x.Name.IsHoustonAnchorAttribute() && !AttributeHasDefaultValue(x) && x.Name != "HideInEditor"))
            {
                Output.AppendLine(ParseAttributeValue(attr, true));
            }

            var children = wndNode.Elements().Where(x => x.Name == "Control");
            var events = wndNode.Elements().Where(x => x.Name == "Event");

            // reverse the order of pixies and columns due to the way DaiGUI handles them.
            var pixies = wndNode.Elements().Where(x => x.Name == "Pixie").Reverse();
            var columns = wndNode.Elements().Where(x => x.Name == "Column").Reverse();

            ProcessFormElement(events, "Events", AddControlEvent);
            ProcessFormElement(pixies, "Pixies", AddPixieOrColumn);
            ProcessFormElement(columns, "Columns", AddPixieOrColumn);
            ProcessFormElement(children, "Children", ProcessWindowNode);

            _indent--;
            Output.Append(IndentString + "}" + (_indent == 0 ? "" : ",")  + "\n");
        }

        private void ProcessFormElement(IEnumerable<XElement> elements, string luaKeyName, Action<XElement> processAction)
        {
            if (!elements.Any()) return;
            Output.Append(IndentString + luaKeyName + " = {\n");
            _indent++;
            foreach (var element in elements)
            {
                processAction(element);
            }
            _indent--;
            Output.Append(IndentString + "},\n");
        }


        private void AddPixieOrColumn(XElement element)
        {
            Output.Append(IndentString + "{ ");

            foreach (var anchorType in new[] {"AnchorOffset", "AnchorPoint"})
            {
                if (!element.Attributes().Any(x => x.Name.ToString().EndsWith(anchorType))) continue;
                var anchor = GetAnchorTable(element.Attributes(), anchorType);
                if (anchor == null) continue;
                Output.AppendFormat("{0} = {1}, ", anchorType + "s", anchor);
            }

            foreach (var attr in element.Attributes().Where(x => !x.Name.IsHoustonAnchorAttribute() && !AttributeHasDefaultValue(x)))
            {
                Output.Append(ParseAttributeValue(attr));
            }

            Output.Append("},\n");
        }

        private string ParseAttributeValue(XAttribute attr, bool shouldIndent = false)
        {
            var strIndent = shouldIndent ? IndentString : "";

            var strValue = attr.Value;
            if (BooleanAttributes.Contains(attr.Name.ToString()))
            {
                strValue = attr.Value == "0" ? "false" : "true";
            }
            else if (!attr.Value.IsDigitsOnly())
            {
                strValue = "\"" + strValue + "\"";
            }

            return string.Format("{0}{1} = {2}, ", strIndent, attr.Name, strValue);
        }

        private string GetAnchorTable(IEnumerable<XAttribute> attrs, string strType)
        {
            var points = new Dictionary<string, object> {{"L", 0}, {"T", 0}, {"R", 0}, {"B", 0}};

            foreach (var strKey in points.Keys.ToArray())
            {
                var attr = attrs.FirstOrDefault(x => x.Name == strKey + strType);
                if (attr == null) continue;

                if (attr.Value.IsDigitsOnly())
                    points[strKey] = attr.Value.ToInt32();
                else
                    points[strKey] = "\"" + attr.Value + "\"";
            }

            // Return null if LTRB all equal 0
            if (points.Values.All(x => x is Int32 && (Int32)x == 0)) return null;

            // Set AnchorPoint to "named" variant if available 
            // e.g. { 0.5, 0.5, 0.5, 0.5 } = "CENTER"
            var strRet = string.Format("{{ {0}, {1}, {2}, {3} }}", points["L"], points["T"], points["R"], points["B"]);
            if (strType == "AnchorPoint" && TranslateAnchorPoints && AnchorPointLookup.ContainsKey(strRet))
            {
                strRet = "\"" + AnchorPointLookup[strRet] + "\"";
            }

            return strRet;
        }

        private void AddControlEvent(XElement xml)
        {
            var strEventName = xml.Attributes().First(x => x.Name == "Name").Value;
            var strEventFunction = xml.Attributes().First(x => x.Name == "Function").Value;
            Output.AppendFormat("{0}{1} = \"{2}\",\n", IndentString, strEventName, strEventFunction);
        }

        #region AnchorPoint Lookup Table
        private static readonly Dictionary<string, string> AnchorPointLookup = new Dictionary<string, string>
            {
                { "{ 0, 0, 0, 0 }" , "TOPLEFT" },
                { "{ 1, 0, 1, 0 }" , "TOPRIGHT" },
                { "{ 0, 1, 0, 1 }" , "BOTTOMLEFT" },
                { "{ 1, 1, 1, 1 }" , "BOTTOMRIGHT" },
                { "{ 0.5, 0.5, 0.5, 0.5 }" , "CENTER" },
                { "{ 0, 0.5, 0, 0.5 }" , "VCENTER" },
                { "{ 1, 0.5, 1, 0.5 }" , "VCENTERRIGHT" },
                { "{ 0.5, 0, 0.5, 0 }" , "HCENTER" },
                { "{ 0.5, 1, 0.5, 1 }" , "HCENTERBOTTOM" },
                { "{ 0, 0, 1, 1 }" , "FILL" },
                { "{ 0, 0, 1, 0 }" , "HFILL" },
                { "{ 0, 0, 0, 1 }" , "VFILL" },
                { "{ 1, 0, 1, 1 }" , "VFILLRIGHT" },
                { "{ 0, 1, 1, 1 }" , "HFILLBOTTOM" },
            };
        #endregion

        #region Boolean Attributes
        private static readonly List<string> BooleanAttributes = new List<string>
            {
                "AlignBuffsRight",
                "AllowEditing",
                "AllowTextEditing",
                "Animated",
                "AutoAddBuffs",
                "AutoDestroyWithUnit",
                "AutoFade",
                "AutoFadeBG",
                "AutoFadeNC",
                "AutoHideScroll",
                "AutoScaleText",
                "AutoSetText",
                "AutoSize",
                "AutoSizeX",
                "AutoSizeY",
                "BRtoLT",
                "BeneficialBuffs",
                "BlockOutIfDisabled",
                "Border",
                "BuffDispellable",
                "BuffNonDispelRightClick",
                "BuffNonDispellable",
                "CheckboxRight",
                "CircularItems",
                "ClearOnEscape",
                "Clockwise",
                "CloseOnExternalClick",
                "DT_BOTTOM",
                "DT_CENTER",
                "DT_RIGHT",
                "DT_SINGLELINE",
                "DT_VCENTER",
                "DT_WORDBREAK",
                "DebuffDispellable",
                "DebuffNonDispellable",
                "DefaultTarget",
                "DisableMousePan",
                "DisableWheelZoom",
                "DisallowDragDrop",
                "DiscreteTicks",
                "DoNotBlockTooltip",
                "DoNotShowTimeRemaining",
                "DrawAsCheckbox",
                "DrawClientSprite",
                "DrawHotkey",
                "DrawObjectsOnContinent",
                "DrawTextOnFlyby",
                "DrawTicks",
                "EdgeGlow",
                "Escapable",
                "FocusOnClick",
                "FocusOnMouseOver",
                "HScroll",
                "HarmfulBuffs",
                "HeaderRow",
                "Hero",
                "IfHoldNoSignal",
                "IgnoreMax",
                "IgnoreMouse",
                "IgnoreTooltipDelay",
                "InstantMouseReact",
                "KeepStringList",
                "Line",
                "ListItem",
                "LoseFocusOnExternalClick",
                "MaintainAspectRatio",
                "MergeLeft",
                "Moveable",
                "MultiColumn",
                "MultiLine",
                "MultiSelect",
                "NeverBringToFront",
                //"NewWindowDepth", // Not a true boolean value as could be a numeric value for the layer/depth order.  Similar to NewControlDepth.
                "NoButtons",
                "NoClip",
                "NoClipEdgeGlow",
                "NoLines",
                "NoScrollbar",
                "NoSelectOnFocus",
                "NoSelection",
                "NotRelative",
                "Overlapped",
                "Password",
                "Picture",
                "PolygonalClipping",
                "ProcessRightClick",
                "PulseWhenExpiring",
                "RadialBar",
                "RadioAlwaysSignal",
                "RadioDisallowNonSelection",
                "ReadOnly",
                "RelativeToClient",
                "ScaleOnShowHide",
                "SelectWholeRow",
                "SetTextToProgress",
                "ShowMS",
                "SimpleSort",
                "Sizable",
                "SizeToFit",
                "SkipZeroes",
                "SwallowMouseClicks",
                "TabStop",
                "TestAlpha",
                // "TooltipForm", // Unsure if boolean value.  Only reference are values of "1" which could mean true like other booleans.
                "TransitionShowHide",
                "UseBaseButtonArt",
                "UseButtons",
                "UseNodeColor",
                "UseParentOpacity",
                "UsePercent",
                "UseRadialClipping",
                "UseTemplateBG",
                "UseTextRect",
                "UseTheme",
                "UseValues",
                "UseWindowTextColor",
                "VScroll",
                "VScrollLeftSide",
                "VariableHeight",
                "VerticallyAlign",
                "Visible",
                "WantReturn",
                "WantTab",
            };
        #endregion

        #region Attribute Default Values
        private static readonly Dictionary<string, string> AttributeDefaults = new Dictionary<string, string>
            {
                { "Class", "Window" },
                { "Font", "Default" },
                { "Text", ""},
                { "Template", "Default" },
                { "TooltipType", "OnCursor"} ,
                { "Border" , "0" },
                { "Picture" , "0" },
                { "SwallowMouseClicks" , "0" },
                { "Moveable" , "0" },
                { "Escapable" , "0" },
                { "Overlapped" , "0" },
                { "BGColor" , "ffffffff" },
                { "TextColor" , "ffffffff" },
                { "TooltipColor" , "" },
                { "Tooltip" , "" },
                { "AutoFade" , "0" },
                { "NoClip" , "0" },
                { "AutoFadeNC" , "0" },
                { "AutoFadeBG" , "0" },
                { "IgnoreMouse" , "0" },
                { "RelativeToClient" , "0" },
                { "UseParentOpacity" , "0" },
                { "AutoHideScroll" , "0" },
                { "TabStop" , "0" },
                { "NeverBringToFront" , "0" },
                { "AutoScaleText" , "0" },
                { "TestAlpha" , "0" },
                { "VScrollLeftSide" , "0" },
                { "UseTemplateBG" , "0" },
                { "DoNotBlockTooltip" , "0" },
                { "UseRadialClipping" , "0" },
                { "BlockOutIfDisabled" , "0" },
                { "IgnoreTooltipDelay" , "0" },
                { "TransitionShowHide" , "0" },
                { "MaintainAspectRatio" , "0" },
                { "ScaleOnShowHide" , "0" },
                { "HScroll" , "0" },
                { "VScroll" , "0" },
                { "NotRelative" , "0" },
                { "Sizable" , "0" },
                { "CloseOnExternalClick" , "0" },
                { "NewWindowDepth" , "0" },
                { "NewControlDepth" , "0" },
                { "Visible" , "1" },
                { "Sprite" , "" },
                { "LeftEdgeControlsAnchor" , "" },
                { "TopEdgeControlsAnchor" , "" },
                { "RightEdgeControlsAnchor" , "" },
                { "BottomEdgeControlsAnchor" , "" },
                { "TextId" , "" },
                { "TooltipId" , "" },
                { "DT_CENTER" , "0" },
                { "DT_VCENTER" , "0" },
                { "DT_RIGHT" , "0" },
                { "DT_BOTTOM" , "0" },
                { "DT_WORDBREAK" , "0" },
                { "DT_SINGLELINE" , "0" },
                { "Line" , "0" },
                { "Rotation" , "0" },
                { "MergeLeft" , "0" },
                { "Image" , "" },
                { "NormalTextColor", "ffffffff" },
                { "PressedTextColor", "ffffffff" },
                { "PressedFlybyTextColor", "ffffffff" },
                { "FlybyTextColor", "ffffffff" },
                { "DisabledTextColor", "ffffffff" },
                { "CellBGNormalColor", ""},
                { "CellBGSelectedColor" , ""},
                { "CellBGNormalFocusColor" , ""},
                { "CellBGSelectedFocusColor" , ""},
                { "TextNormalColor" , ""},
                { "TextSelectedColor" , ""},
                { "TextNormalFocusColor" , ""},
                { "TextSelectedFocusColor" , ""},
                { "RadioGroup" , ""},
            };
        #endregion
    }
}
