using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace scorpion_gui
{
    partial class Form1
    {
        private void draw_element(Element e, int screen_width, int screen_height)
        {
            int procentX = screen_width / 100;
            int procentY = screen_height / 100;
            procentX = procentX * e.x;
            procentY = procentY * e.y;

            int procentWidth = (screen_width / 100) * e.width;
            int procentHeight = (screen_height / 100) * e.height;

            List<double> point1 = new List<double>();
            List<double> point2 = new List<double>();
            List<double> point3 = new List<double>();
            List<double> point4 = new List<double>();
            point1.Add(procentX);
            point1.Add(procentY);

            point2.Add(procentX);
            point2.Add(procentY + procentHeight);

            point3.Add(procentX + procentWidth);
            point3.Add(procentY + procentHeight);

            point4.Add(procentX + procentWidth);
            point4.Add(procentY);

            e.element_coordinates.Add(point1);
            e.element_coordinates.Add(point2);
            e.element_coordinates.Add(point3);
            e.element_coordinates.Add(point4);

            GL.Color3(e.color);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(e.element_coordinates[0][0], e.element_coordinates[0][1]);
            GL.Vertex2(e.element_coordinates[1][0], e.element_coordinates[1][1]);
            GL.Vertex2(e.element_coordinates[2][0], e.element_coordinates[2][1]);
            GL.Vertex2(e.element_coordinates[3][0], e.element_coordinates[3][1]);
            GL.End();

            return;
        }

        private void draw_gui()
        {
            foreach (Element e in AL_OBJ_3D)
            {
                draw_element(e, Size.Width, Size.Height);
            }

            return;
        }
    }
}
