using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UploadingUploadingService" in code, svc and config file together.
public class UploadingUploadingService : IUploadingService
{
    private static string _thisfileName = "base.txt";
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}

    // =========================================================================
    //private string path = @"c:\temp";
    string path = HttpRuntime.AppDomainAppPath + "\\TextDocumentHolder"; // File path to document holder
    public string StreamToFile(Stream input)
    {
        string fileName;
        if (_thisfileName == "base.txt")
        {
            fileName =
                String.Format(@"{0}\{1}.txt", path,
                    Guid.NewGuid().ToString()); // Create the filepath with a new file name
        }
        else
        {
            fileName = String.Format(@"{0}\{1}", path, _thisfileName);
        }

        StreamReader reader = new StreamReader(input); // Create a stream reader and pass in the input stream
        var content = reader.ReadToEnd(); // Create a string from the stream
        File.WriteAllText(fileName, content); // Write all of the text into a file at the specified file path
        return fileName;

    }

    public void SetFileName(string fileName)
    {
        _thisfileName = fileName;
    }
    
    public Stream FilePathToStream(string file)
    {
		MemoryStream stream = new MemoryStream(); // Create a stream in memory
        var bytes = File.ReadAllBytes(file); // Convert the contents of the file into bytes and story into array
		stream.Write(bytes, 0, bytes.Length); // Write the contents of the byte[] to the memory stream
        stream.Position = 0; // Reset the position of the stream pointer back to the head of the stream
        return stream; // Return the memory stream with the contents of the file
    }

    public string StringToFile(string input)
    {
        /*
            Takes a file path but separator need an escape char \\
            StreamToFile(FilePathToStream(input));
        */

        MemoryStream stream = new MemoryStream(); // Create a stream in memory
        //var bytes = File.ReadAllBytes(file); // Convert the contents of the file into bytes and story into array
        var bytes = Encoding.ASCII.GetBytes(input);
        stream.Write(bytes, 0, bytes.Length); // Write the contents of the byte[] to the memory stream
        stream.Position = 0; // Reset the position of the stream pointer back to the head of the stream
        string response = StreamToFile(stream);

        return response;
    }

    public string FilePathToFile(string filePath)
    {
        try
        {
            string writtenTo = StreamToFile(FilePathToStream(filePath));
            return writtenTo;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
