using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Gostar.Common
{
    public static class ColorsAndStyles
    {
        #region BorderWidth
        public static int ControlLineWidth
        {
            get { return 2; } // Pixceks
        }

        public static int GridNormalLineWidth
        {
            get { return 2; }
        }
        public static int GridSelectLineWidth
        {
            get { return 2; }
        }



        public static Color GridRowBackColorHowerSelect
        {
            get { return Color.FromArgb(223, 243, 254); }
        }

        public static Color SpaceLineColor
        {
            get { return Color.FromArgb(23, 23, 23); }
        }

        public static Color GridRowForeColorSelect
        {
            get { return Color.DarkBlue; /*ColorTranslator.FromHtml("#2979FF");*//*Color.FromArgb(16, 156, 241);*/ }
        }

        #endregion

        #region NormalColors
        public static Color Controls_Normal_LblColor
        {
            get { return ColorTranslator.FromHtml("#323c47"); }
        }

        public static Color Controls_Normal_LineColor
        {
            get { return ColorTranslator.FromHtml("#323c47"); }
        }

        public static Color Controls_Normal_ForeColor
        {
            get { return ColorTranslator.FromHtml("#323c47"); }
        }
        #endregion

        #region SelectColors
        public static Color Controls_Select_LblColor
        {
            get { return ColorTranslator.FromHtml("#323c47"); }
        }

        public static Color Controls_Select_LineColor
        {
            get { return ColorTranslator.FromHtml("#323c47"); }
        }

        public static Color Controls_Select_ForeColor
        {
            get { return ColorTranslator.FromHtml("#323c47"); }
        }
        #endregion

        #region CompleteColors
        public static Color Controls_Complete_LblColor
        {
            get { return ColorTranslator.FromHtml("#707683"); }
        }

        public static Color Controls_Complete_LineColor
        {
            get { return Color.DarkGreen; /*ColorTranslator.FromHtml("#3D6647");*/ }
        }

        public static Color Controls_Complete_ForeColor
        {
            get { return Color.DarkGreen; /*ColorTranslator.FromHtml("#3D6647");*/ }
        }
        #endregion

        public static Color Controls_CheckedDropDown_BackItem
        {
            get { return Color.DarkGreen; /*ColorTranslator.FromHtml("#3D6647");*/ }
        }

    }
}
