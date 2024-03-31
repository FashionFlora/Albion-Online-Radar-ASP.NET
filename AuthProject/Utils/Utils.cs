namespace AuthProject.Utils
{
    public class Utils
    {

        public static  string[] ReadConfigFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
            
                    return File.ReadAllLines(filePath);

       
                }
                else
                {
                    Console.WriteLine("The file does not exist: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return null;

        }
    }
}
