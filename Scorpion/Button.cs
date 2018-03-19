using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace scorpion_gui
{
    class Button : Element
    {
        public List<List<double>> buttonCoordinates = new List<List<double>>();
        public Color buttonColor = Color.Chocolate;

        public int width;
        public int height;

       // public int x;
      //  public int y;

        public void define(int width, int height, int x, int y)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
        }
    
        /*
        public void drawButton(int screenWidth, int screenHeight)
        {
            int procentX = screenWidth / 100;
            int procentY = screenHeight / 100;
            procentX = procentX * x;
            procentY = procentY * y;

            int procentWidth = (screenWidth / 100) * width;
            int procentHeight = (screenWidth / 100) * height;

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

            buttonCoordinates.Add(point1);
            buttonCoordinates.Add(point2);
            buttonCoordinates.Add(point3);
            buttonCoordinates.Add(point4);
            GL.Color3(buttonColor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(buttonCoordinates[0][0], buttonCoordinates[0][1]);
            GL.Vertex2(buttonCoordinates[1][0], buttonCoordinates[1][1]);
            GL.Vertex2(buttonCoordinates[2][0], buttonCoordinates[2][1]);
            GL.Vertex2(buttonCoordinates[3][0], buttonCoordinates[3][1]);
            GL.End();
        }
        

        public void drawButton2(int width, int height, int x, int y, int screenWidth, int screenHeight)
        {
            int procentX = screenWidth / 100;
            int procentY = screenHeight / 100;
            procentX = procentX * x;
            procentY = procentY * y;

            int procentWidth = (screenWidth / 100) * width;
            int procentHeight = (screenWidth / 100) * height;

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

            buttonCoordinates.Add(point1);
            buttonCoordinates.Add(point2);
            buttonCoordinates.Add(point3);
            buttonCoordinates.Add(point4);
            GL.Color3(buttonColor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(buttonCoordinates[0][0], buttonCoordinates[0][1]);
            GL.Vertex2(buttonCoordinates[1][0], buttonCoordinates[1][1]);
            GL.Vertex2(buttonCoordinates[2][0], buttonCoordinates[2][1]);
            GL.Vertex2(buttonCoordinates[3][0], buttonCoordinates[3][1]);
            GL.End();
        }
        */

        public Button()
        {
            width = 10;
            height = 10;
            x = 50;
            y = 50;

            return;
        }
        public Button(int width, int height, int x, int y)
        {
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;

            return;
        }
    }


}
