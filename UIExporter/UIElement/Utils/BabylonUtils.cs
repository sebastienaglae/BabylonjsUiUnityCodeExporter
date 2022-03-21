using System;
using UnityEngine;

namespace PROJECT
{
    public static class BabylonUtils
    {
        public static void CreateCodeAffectation(string varName, string properties, string affect, ref string n)
        {
            n += $"{varName}.{properties} = {affect};\n";
        }

        public static void CreateCodeMethod(string varName, string method, string arg, ref string n)
        {
            n += $"{varName}.{method}({arg});\n";
        }

        public static void CreateCodeProperty(string varName, string properties, string value, bool withQuotationMark,
            ref string n)
        {
            value = CanvasExporterUtils.ConvertEmptyNullWhiteSpace(value);
            if (withQuotationMark)
                n += $"{varName}.{properties} = \"{CanvasExporterUtils.EncodeLine(value)}\";\n";
            else
                n += $"{varName}.{properties} = {CanvasExporterUtils.EncodeLine(value)};\n";
        }

        public static void CreateCodeProperty(string varName, string properties, Enum value, ref string n)
        {
            n += $"{varName}.{properties} = \"{value.ToString().ToLower().Replace("_", "-")}\";\n";
        }

        public static void CreateCodeProperty(string varName, string properties, float value, ref string n)
        {
            n += $"{varName}.{properties} = {CanvasExporterUtils.Convert(value)};\n";
        }

        public static void CreateCodeProperty(string varName, string properties, int value, ref string n)
        {
            n += $"{varName}.{properties} = {value};\n";
        }

        public static void CreateCodeProperty(string varName, string properties, int value, string unit, ref string n)
        {
            n += $"{varName}.{properties} = \"{value}{unit}\";\n";
        }

        public static void CreateCodeProperty(string varName, string properties, float value, string unit, ref string n)
        {
            n += $"{varName}.{properties} = \"{CanvasExporterUtils.Convert(value)}{unit}\";\n";
        }

        public static void CreateCodeProperty(string varName, string properties, Color value, ref string n)
        {
            n += $"{varName}.{properties} = \"#{CanvasExporterUtils.ColorConvert(value)}\";\n";
        }

        public static void CreateCodeProperty(string varName, string properties, Color32 value, ref string n)
        {
            n += $"{varName}.{properties} = \"#{CanvasExporterUtils.ColorConvert(value)}\";\n";
        }

        public static void CreateCodeProperty(string varName, string properties, bool value, ref string n)
        {
            n += $"{varName}.{properties} = {value.ToString().ToLower()};\n";
        }

        public struct Unit
        {
            public const string PERCENT = "%";
            public const string PIXEL = "px";
            public const string ELEMENT_RELATIVE = "em";
            public const string POINTS = "pt";
            public const string PICAS = "pc";
            public const string INCHES = "in";
            public const string QUART_MIL = "Q";
            public const string MILLIMITER = "mm";
            public const string CENTIMETER = "cm";
            public const string VIEWPORT_HEIGHT = "vh";
            public const string VIEWPORT_WIDTH = "vw";
        }
    }
}