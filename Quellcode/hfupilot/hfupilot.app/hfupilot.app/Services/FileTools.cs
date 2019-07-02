using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace hfupilot.app.Services
{
    public class FileTools
    {
        
            public static void Save(string filename, byte[] content)
            {
                var path = GetPath(filename);
                File.WriteAllBytes(path, content);
            }

            public static bool Exists(string filename) => File.Exists(GetPath(filename));

            public static byte[] Read(string filename)
            {
                var path = GetPath(filename);

                if (Exists(filename))
                {
                    return File.ReadAllBytes(path);
                }

                return new byte[0];
            }

            private static string GetPath(string filename) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
        }

}

