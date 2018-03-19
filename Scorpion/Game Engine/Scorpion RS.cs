using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using OpenTK;
using System.Drawing;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace Scorpion.Game_Engine
{
    public class Scorpion_RS
    {
        Form1 Do_on;
        public Scorpion_RS(Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void load_game(ref string database)
        {
            /*
            Every section has an init variable the init variable lists all objects to be loaded when the db is loaded into memory
            */
            using (Scorpion_Game sc = new Scorpion_Game())
            {
                sc.Run(60.0);
            }

            return;
        }

        //OpenGL
        public void start_Game_engine()
        {
            using (Do_on.game = new GameWindow())
            {
                Do_on.game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    Do_on.game.VSync = VSyncMode.Adaptive;
                };

                Do_on.game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, Do_on.game.Width, Do_on.game.Height);
                };

                Do_on.game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (Do_on.game.Keyboard[Key.Escape])
                    {
                        Do_on.game.Exit();
                    }
                };

                Do_on.game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    //GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                    //Load Vectors Instance.
                    read_objects();
                    read_keys();

                    GL.Begin(PrimitiveType.Lines);

                    GL.Color3(Color.Black);
                    GL.Vertex2(-1.0f, 1.0f);
                    GL.Color3(Color.White);
                    GL.Vertex2(0.0f, -1.0f);
                    GL.Color3(Color.Black);
                    GL.Vertex2(1.0f, 1.0f);

                    GL.End();

                    Do_on.game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                Do_on.game.Run(60.0);
            }
            return;
        }

        public void read_keys()
        {

            return;
        }

        public void read_objects()
        {

            return;
        }

        public void stop_Game_engine()
        {
            Do_on.game.Exit();
            return;
        }

        public void set_Game_vsync(ref string Scorp_Line)
        {
            //(*db@table@vsync)
            Do_on.game.VSync = (VSyncMode)Do_on.readr.lib_SCR.cut_variables(ref Scorp_Line)[0];

            return;
        }

        public void set_resolution(ref string Scorp_Line)
        {
            //(*x,*y)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line);
           

            return;
        }

        public void reset_all_resolutions()
        {
            //()
            foreach (DisplayDevice d in Do_on.AL_DISP_DEVICES)
            {
                d.RestoreResolution();
            }
            return;
        }

    }

    class Scorpion_Game : OpenTK.GameWindow
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "Scorpion RS";
            GL.ClearColor(Color.Black);
            return;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Draw Model
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Triangles);

            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, 1.0f, 4.0f);

            GL.End();

            SwapBuffers();
            return;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(this.ClientRectangle);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            return;
        }
    }
}
