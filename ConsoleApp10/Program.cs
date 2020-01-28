using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace ConsoleApp10
{
    sealed class Program
    {
        // Declared static (no need for object reference
        static float X = 0.0f;        // Translate screen to x direction (left or right)
        static float Y = 0.0f;        // Translate screen to y direction (up or down)
        static float Z = 0.0f;        // Translate screen to z direction (zoom in or out)
        static float rotX = 0.0f;    // Rotate screen on x axis 
        static float rotY = 0.0f;    // Rotate screen on y axis
        static float rotZ = 0.0f;    // Rotate screen on z axis

        static float rotLx = 0.0f;   // Translate screen by using the glulookAt function 
                                     // (left or right)
        static float rotLy = 0.0f;   // Translate screen by using the glulookAt function 
                                     // (up or down)
        static float rotLz = 0.0f;   // Translate screen by using the glulookAt function 
                                     // (zoom in or out)

        static bool lines = true;       // Display x,y,z lines (coordinate lines)
        static bool rotation = false;   // Rotate if F2 is pressed   
        static int M = 100, N = 100;
        static float[,] P = {
        { 0.0f, 10.0f, 0.0f },    //p(0,0)
        
        { 0.0f, 10.0f, 10.0f },  //p(0,1)
        { 10.0f, 0.0f, 0.0f },    //p(1,0)
        { 0.0f, 0.0f, 10.0f }     //p(1,1)
        };

        static float[,] Q = new float [M*N, 3];
        // Draw the lines (x,y,z)
        static void drawings()
        {
            // Clear the Color Buffer and Depth Buffer
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glPushMatrix();   // It is important to push the Matrix before 
                                 // calling glRotatef and glTranslatef
            Gl.glRotatef(rotX, 1.0f, 0.0f, 0.0f);            // Rotate on x
            Gl.glRotatef(rotY, 0.0f, 1.0f, 0.0f);            // Rotate on y
            Gl.glRotatef(rotZ, 0.0f, 0.0f, 1.0f);            // Rotate on z

            if (rotation) // If F2 is pressed update x,y,z for rotation of the cube
            {
                rotX += 0.2f;
                rotY += 0.2f;
                rotZ += 0.2f;
            }

            Gl.glTranslatef(X, Y, Z);        // Translates the screen left or right, 
                                             // up or down or zoom in zoom out

            if (lines)  // If F1 is pressed don't draw the lines
            {
                // Draw the positive side of the lines x,y,z
                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(0.0f, 1.0f, 0.0f);                // Green for x axis
                Gl.glVertex3f(0f, 0f, 0f);
                Gl.glVertex3f(10f, 0f, 0f);
                Gl.glColor3f(1.0f, 0.0f, 0.0f);                // Red for y axis
                Gl.glVertex3f(0f, 0f, 0f);
                Gl.glVertex3f(0f, 10f, 0f);
                Gl.glColor3f(0.0f, 0.0f, 1.0f);                // Blue for z axis
                Gl.glVertex3f(0f, 0f, 0f);
                Gl.glVertex3f(0f, 0f, 10f);
                Gl.glEnd();

                // Dotted lines for the negative sides of x,y,z coordinates
                Gl.glEnable(Gl.GL_LINE_STIPPLE); // Enable line stipple to use a 
                                                 // dotted pattern for the lines
                Gl.glLineStipple(1, 0x0101);     // Dotted stipple pattern for the lines
                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(0.0f, 1.0f, 0.0f);                    // Green for x axis
                Gl.glVertex3f(-10f, 0f, 0f);
                Gl.glVertex3f(0f, 0f, 0f);
                Gl.glColor3f(1.0f, 0.0f, 0.0f);                    // Red for y axis
                Gl.glVertex3f(0f, 0f, 0f);
                Gl.glVertex3f(0f, -10f, 0f);
                Gl.glColor3f(0.0f, 0.0f, 1.0f);                    // Blue for z axis
                Gl.glVertex3f(0f, 0f, 0f);
                Gl.glVertex3f(0f, 0f, -10f);
                Gl.glEnd();
            }

            // I start to draw my 3D cube
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3f(1.0f, -0.1f, -0.1f);
            Gl.glVertex3f(-0.1f, -0.1f, -0.1f);
            Gl.glVertex3f(10.0f, -0.1f, -0.1f);
            Gl.glVertex3f(10.0f, 10.0f, -0.1f);
            Gl.glVertex3f(-0.1f, 10.0f, -0.1f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3f(1.0f, -0.1f, -0.1f);
            Gl.glVertex3f(-0.1f, -0.1f, -0.1f);
            Gl.glVertex3f(10.0f, -0.1f, -0.1f);
            Gl.glVertex3f(10.0f, -0.1f, 10.0f);
            Gl.glVertex3f(-0.1f, -0.1f, 10.0f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3f(1.0f, -0.1f, -0.1f);
            Gl.glVertex3f(-0.1f, -0.1f, -0.1f);
            Gl.glVertex3f(-0.1f, 10.0f, -0.1f);
            Gl.glVertex3f(-0.1f, 10.0f, 10.0f);
            Gl.glVertex3f(-0.1f, -0.1f, 10.0f);
            Gl.glEnd();

            Gl.glDisable(Gl.GL_LINE_STIPPLE);   // Disable the line stipple


            Gl.glBegin(Gl.GL_POINTS);
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            //Gl.glVertex3f(10.0f, 10.0f, 10.0f);
            float U, W;
                int K=0;
            for (int i = 0; i < N; i++)
            {
                U = (float)i / N;
                for (int j = 0; j < M; j++)
                {
                    W = (float)j / M;
                    Q[K, 0] =   P[0, 0] * (1 - U) * (1 - W) + 
                                P[1, 0] * (1 - U) * W + 
                                P[2, 0] * U * (1 - W) + 
                                P[3, 0] * U * W;

                    Q[K, 1] =   P[0, 1] * (1 - U) * (1 - W) +
                                P[1, 1] * (1 - U) * W +
                                P[2, 1] * U * (1 - W) +
                                P[3, 1] * U * W;

                    Q[K, 2] =   P[0, 2] * (1 - U) * (1 - W) +
                                P[1, 2] * (1 - U) * W +
                                P[2, 2] * U * (1 - W) +
                                P[3, 2] * U * W;
                    Gl.glVertex3f(Q[K,0], Q[K, 1], Q[K, 2]);
                    K++;
                }
            }
            Gl.glEnd();


            Glut.glutPostRedisplay();           // Redraw the scene
            Gl.glPopMatrix();                   // Don't forget to pop the Matrix
            Glut.glutSwapBuffers();
        }
        // Initialize the OpenGL window
        static void init()
        {
            Gl.glShadeModel(Gl.GL_SMOOTH);     // Set the shading model to smooth 
            Gl.glClearColor(0, 0, 0, 0.0f);    // Clear the Color
            // Clear the Color and Depth Buffer
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearDepth(1.0f);          // Set the Depth buffer value (ranges[0,1])
            Gl.glEnable(Gl.GL_DEPTH_TEST);  // Enable Depth test
            Gl.glDepthFunc(Gl.GL_LEQUAL);   // If two objects on the same coordinate 
                                            // show the first drawn
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);
        }

        // This function is called whenever the window size is changed
        static void reshape(int w, int h)
        {
            Gl.glViewport(0, 0, w, h);                // Set the viewport
            Gl.glMatrixMode(Gl.GL_PROJECTION);        // Set the Matrix mode
            Gl.glLoadIdentity();
            Glu.gluPerspective(75f, (float)w / (float)h, 0.10f, 500.0f);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(rotLx, rotLy, 30.0f +
                     rotLz, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
        }

        // This function is used for the navigation keys
        public static void keyboard(byte key, int x, int y)
        {
            switch (key)
            {
                // x,X,y,Y,z,Z uses the glRotatef() function
                case 120:    // x             // Rotates screen on x axis 
                    rotX -= 2.0f;
                    break;
                case 88:    // X            // Opposite way 
                    rotX += 2.0f;
                    break;
                case 121:    // y            // Rotates screen on y axis
                    rotY -= 2.0f;
                    break;
                case 89:    // Y            // Opposite way
                    rotY += 2.0f;
                    break;
                case 122:    // z            // Rotates screen on z axis
                    rotZ -= 2.0f;
                    break;
                case 90:    // Z            // Opposite way
                    rotZ += 2.0f;
                    break;

                case 111:    // o        // Resets all parameters
                case 80:    // O        // Displays the cube in the starting position
                    rotation = false;
                    X = Y = 0.0f;
                    Z = 0.0f;
                    rotX = 0.0f;
                    rotY = 0.0f;
                    rotZ = 0.0f;
                    rotLx = 0.0f;
                    rotLy = 0.0f;
                    rotLz = 0.0f;
                    Gl.glMatrixMode(Gl.GL_MODELVIEW);
                    Gl.glLoadIdentity();
                    Glu.gluLookAt(rotLx, rotLy, 15.0f + rotLz,
                        0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
                    break;
            }
            Glut.glutPostRedisplay();    // Redraw the scene
        }


        // Main Starts
        static void Main(string[] args)
        {
            Glut.glutInit();        // Initialize glut
            // Setup display mode to double buffer and RGB color
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGB);
            // Set the screen size
            Glut.glutInitWindowSize(1200, 700);
            Glut.glutCreateWindow("OpenGL 3D Navigation Program With Tao");
            init();
            Glut.glutReshapeFunc(reshape);
            Glut.glutDisplayFunc(drawings);
            // Set window's key callback
            Glut.glutKeyboardFunc(new Glut.KeyboardCallback(keyboard));
            Glut.glutMainLoop();
        }
    }
}
