/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

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

namespace Scorpion
{
    partial class Librarian
    {
        public void generatersakeys(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Please run the following script using scriptrun for a Private key: ", "processdefine::*'openssl', *f'genrsa -out {((path))}/RSA/private-key.pem 2048', *'genrsapriv', *false, *false >> processrun::*'genrsapriv'");
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("\v", "Please run the following script using scriptrun for a Public key: ", "processdefine::*'openssl', *f'rsa -in {((path))}/RSA/private-key.pem -pubout -out {((path))}/RSA/public-key.pem', *'genrsapub', *false, *false >> processrun::*'genrsapriv'");
            return;
        }
    }
}