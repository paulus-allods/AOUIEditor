using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AOUIEditor.ResourceSystem
{
    public class WidgetLayerTiledTexture : WidgetLayer
    {
        [Description("Текстура")]
        public UITextureItem textureItem { get; set; }

        [Description("Описание тайлинга. Прямоугольник текстуры разбивается на 9 частей (3 части по вертикали и 3 по горизонтали). Угловые части не тайлятся. оставшиеся краевые части тайлятся соответственно по горизонтали и по вертикали. Центральная часть тайлится на всю оставшуюся поверхность")]
        public WidgetLayerTiledLayout Layout { get; set; }

        [DefaultValue(WidgetLayerTiledLayoutType.WIDGET_LAYER_TILED_LAYOUT_TYPE_TILED)]
        [Description("Способ вывода текстуры по горизонтали")]
        public WidgetLayerTiledLayoutType? layoutTypeX { get; set; }

        [DefaultValue(WidgetLayerTiledLayoutType.WIDGET_LAYER_TILED_LAYOUT_TYPE_TILED)]
        [Description("Способ вывода текстуры по вертикали")]
        public WidgetLayerTiledLayoutType? layoutTypeY { get; set; }

        public WidgetLayerTiledTexture()
        {
            Layout = new WidgetLayerTiledLayout();
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (Layout.MiddleX != 0)
            {
                if (!IsPowerOfTwo(Layout.MiddleX))
                {
                    Logger.Log($"MiddleX не равен степени 2 у текстуры {Path.Combine(directory, file)}");
                }
                else if (Layout.MiddleX < 32)
                {
                    Logger.Log($"MiddleX меньше 32 у текстуры {Path.Combine(directory, file)}");
                }    
            }
            if (Layout.MiddleY != 0)
            {
                if (!IsPowerOfTwo(Layout.MiddleY))
                {
                    Logger.Log($"MiddleY не равен степени 2 у текстуры {Path.Combine(directory, file)}");
                }
                else if (Layout.MiddleY < 32)
                {
                    Logger.Log($"MiddleY меньше 32 у текстуры {Path.Combine(directory, file)}");
                }
            }
        }

        private bool IsPowerOfTwo(int x)
        {
            return (x & x - 1) == 0;
        }
    }

    public enum WidgetLayerTiledLayoutType
    {
        WIDGET_LAYER_TILED_LAYOUT_TYPE_TILED = 0,
        WIDGET_LAYER_TILED_LAYOUT_TYPE_SCALED = 1
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WidgetLayerTiledLayout
    {
        public int LeftX { get; set; }
        public int MiddleX { get; set; }
        public int RightX { get; set; }
        public int TopY { get; set; }
        public int MiddleY { get; set; }
        public int BottomY { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
