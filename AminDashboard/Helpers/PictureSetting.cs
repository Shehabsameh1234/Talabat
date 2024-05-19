namespace AminDashboard.Helpers
{
	public class PictureSetting
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			//1-get located folder path
			string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);
			//2-get File Name and Make It Unique
			//string fileName=file.Name; //get file Extention
			string fileName = $"{Guid.NewGuid()}{file.FileName}"; //get file Name and make it unique
																  //3-get file path
			string filePath = Path.Combine(folderpath, fileName);
			//4-save file as streams
			using var fileStream = new FileStream(filePath, FileMode.Create);
			file.CopyTo(fileStream);
			return Path.Combine("images\\products" , fileName);
        }

        public static void DeleteFile(string fileName)
		{
			//1-get filepath
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", fileName);
			//2-check if file exist
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
	}
}
