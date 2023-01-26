namespace AgencyTemplate.Utilies
{
    public static class FileExtension
    {
        public static bool CheckType(this IFormFile file, string type) => file.ContentType.Contains(type);
        public static bool CheckSize(this IFormFile file, int kb) => kb * 1024 > file.Length;

        static string ChangeFileName(string oldName)
        {
            string extension = oldName.Substring(oldName.IndexOf('.'));
            if(oldName.Length < 32)
            {
                oldName = oldName.Substring(0,oldName.IndexOf('.'));
            }
            else
            {
                oldName = oldName.Substring(0, 31);
            }
            string newName = Guid.NewGuid()+oldName+extension;
            return newName;
        }

        public static string SaveFile(this IFormFile file, string path)
        {
            string fileName = ChangeFileName(file.FileName);
            using (FileStream fs = new FileStream(Path.Combine(path,fileName) ,FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }

        public static void DeleteFile(this string file, string root, string folder)
        {
            string path = Path.Combine(root, folder, file);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public static string CheckValidate(this IFormFile file, string type, int kb)
        {
            string result = "";
            if (!file.CheckType(type))
            {
                result += "Type is wrong";
            }
            if (!file.CheckSize(kb))
            {
                result += "Size is wrong";
            }
            return result;
        }
    }
}
