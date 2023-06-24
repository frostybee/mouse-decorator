﻿using System.ComponentModel;
using FriskyMouse.Drawing.Attributes;

namespace FriskyMouse.Drawing.Ripples
{
    public enum RippleProfileType : uint
    {
        [Description("Ice Cream"), ConstructableEnum(typeof(IceCreamProfile))]
        IceCream,
        [Description("Expanding Ripple"), ConstructableEnum(typeof(CircleProfile))]
        Ripple,
        [Description("Expanding Square"), ConstructableEnum(typeof(SquareProfile))]
        Square,
        [Description("Hexagon"), ConstructableEnum(typeof(HexagonProfile))]
        Hexagon,
        [Description("Diamond"), ConstructableEnum(typeof(DiamondProfile))]
        Diamond,
        [Description("Sonar Pulse"), ConstructableEnum(typeof(SonarPulseProfile))]
        SonarPulse,        
        [Description("Cherry Blossom"), ConstructableEnum(typeof(CherryBlossomProfile))]
        Cherry,        
        [Description("Star ripple"), ConstructableEnum(typeof(StarProfile))]
        Star,        
        [Description("Fading Spotlight"), ConstructableEnum(typeof(SpotlightProfile))]
        Spotlight,
        [Description("Filled Sonar Pulse"), ConstructableEnum(typeof(FilledSonarPulseProfile))]
        FilledSonarPulse,
        [Description("Squared Pulse"), ConstructableEnum(typeof(SquaredPulseProfile))]
        SquaredPulse,      
        [Description("Crosshair"), ConstructableEnum(typeof(CrosshairProfile))]
        Crosshair,
        [Description("Lollipop"), ConstructableEnum(typeof(ConcentricProfile))]
        Concentric // Core circle in the middle.          
    }

    public enum ShapeType : uint
    {
        Ellipse,        
        Rectangle,
        Polygon,
        Crosshair
    }

    public enum PolygonType : uint
    {
        Diamond,
        Hexagon,
        Star,
    }
}