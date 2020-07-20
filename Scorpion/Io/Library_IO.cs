/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark> <Benjamin Jack Johnson>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections;
using System.Windows.Forms;
using System.IO;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        private string read_file(string path)
        {
            FileStream fd = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            string s_read = sr.ReadToEnd();
            sr.Close();
            fd.Close();

            path = null;
            return s_read;
        }

        //Possible memory leak
        private StreamReader get_sr(string path)
        {
            FileStream fd = new FileStream(path, FileMode.Open);
            path = null;
            return new StreamReader(fd, System.Text.Encoding.UTF8);
        }

        public void Read_File_Text(string Scorp_Line_Exec, int index)
        {
            //io.rft(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            FileStream fs = new FileStream(var_get(al[0].ToString()).ToString(), FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string RSA = sr.ReadToEnd();
            fs.Flush();
            sr.Close();
            fs.Close();

            Do_on.AL_CURR_VAR[index] = (object)RSA;

            //clean
            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;
            RSA = null;

            return;
        }

        public void Read_File_Binary(string Scorp_Line_Get, int index)
        {
            //io.rfb(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Get);

            byte[] byter = File.ReadAllBytes(var_get(al[0].ToString()).ToString());

            Do_on.AL_CURR_VAR[index] = (object)byter;

            //claen
            var_arraylist_dispose(ref al);
            Scorp_Line_Get = null;

            return;
        }

        private void Write_File_Text(string Scorp_Line_Exec)
        {
            //io.sft(path(*),text(*),mode(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            FileMode fm = FileMode.OpenOrCreate;
            try
            {
                if (var_get(al[2].ToString()) == var_get("*append"))
                {
                    fm = FileMode.Append;
                }
            }
            catch { }

            FileStream fs = new FileStream(var_get(al[0].ToString()).ToString(), fm, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(var_get(al[1].ToString()).ToString());
            sw.Flush();
            fs.Flush();
            sw.Close();
            fs.Close();

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Write_File_Binary(string Scorp_Line_Exec)
        {
            //io.sfb(path(*),byte(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            byte[] b = (byte[])var_get(al[1].ToString());
            File.WriteAllBytes(al[0].ToString(), b);

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        public void createfile(string Scorp_Line_Exec, ArrayList objects)
        {
            //io.cf(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            File.Create(var_get(al[0].ToString()).ToString());

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Delete_File(string Scorp_Line_Exec)
        {
            //io.df(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            File.Delete(var_get(al[0].ToString()).ToString());

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Clear_File(string Scorp_Line_Exec)
        {
            //clf(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            FileStream fs = new FileStream(var_get(al[0].ToString()).ToString(), FileMode.Truncate);
            fs.Flush();
            fs.Close();

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Get_Files(string Scorp_Line_Exec, int index)
        {
            //io.gd(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            foreach (FileInfo fnf in new DirectoryInfo(var_get(al[0].ToString()).ToString()).GetFiles())
            {
                Do_on.AL_CURR_VAR[index] = Do_on.AL_CURR_VAR[index].ToString() + Do_on.types.C_Delim_Start + fnf.FullName + Do_on.types.C_Delim_Stop;
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Get_Directories(string Scorp_Line_Exec, int index)
        {
            //io.gf(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            foreach (DirectoryInfo df in new DirectoryInfo(var_get(al[0].ToString()).ToString()).GetDirectories())
            {
                Do_on.AL_CURR_VAR[index] = Do_on.AL_CURR_VAR[index].ToString() + Do_on.types.C_Delim_Start + df.FullName + Do_on.types.C_Delim_Stop;
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Copy_File(string Scorp_Line_Exec)
        {
            //io.cp(path(*),to(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            if (File.Exists(var_get(al[0].ToString()).ToString()) == false || DialogResult.Yes == MessageBox.Show("The file '" + var_get(al[0].ToString()).ToString() + "' already exists, continue copy?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                File.Copy(var_get(al[0].ToString()).ToString(), var_get(al[1].ToString()).ToString(), true);
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Move_File(string Scorp_Line_Exec)
        {
            //io.mp(path(*),to(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            if (File.Exists(var_get(al[0].ToString()).ToString()) == false || DialogResult.Yes == MessageBox.Show("The file '" + var_get(al[0].ToString()).ToString() + "' already exists, continue move?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                File.Move(var_get(al[0].ToString()).ToString(), var_get(al[0].ToString()).ToString());
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        //Directories
        private void Create_Directory(string Scorp_Line_Exec)
        {
            //io.cd(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            Directory.CreateDirectory(var_get(al[0].ToString()).ToString());

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Move_Directory(string Scorp_Line_Exec)
        {
            //io.md(path(*),to(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            Directory.Move(var_get(al[0].ToString()).ToString(), var_get(al[1].ToString()).ToString());

            //clear
            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }

        private void Copy_Directory(string Scorp_Line_Exec)
        {
            //io.cpd(path(*),to(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(var_get(al[0].ToString()).ToString(), "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(var_get(al[0].ToString()).ToString(), var_get(al[1].ToString()).ToString()));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(var_get(al[0].ToString()).ToString(), "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(var_get(al[0].ToString()).ToString(), var_get(al[1].ToString()).ToString()), true);

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void Delete_Directory(string Scorp_Line_Exec)
        {
            //io.dd(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            if (DialogResult.Yes == MessageBox.Show("Remove the folder '" + var_get(al[0].ToString()) + "'", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Directory.Delete(var_get(al[0].ToString()).ToString());
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }
    }
}