using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services.Description;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUploadingService" in both code and config file together.
[ServiceContract]
public interface IUploadingService
{

	[OperationContract]
	string GetData(int value);

	[OperationContract]
	CompositeType GetDataUsingDataContract(CompositeType composite);

    // TODO: Add your service operations here
    [OperationContract]
    string StreamToFile(Stream input);

    [OperationContract]
    Stream FilePathToStream(string file);

	[OperationContract]
    string StringToFile(string input);

    [OperationContract]
    string FilePathToFile(string testInput);

    [OperationContract]
    void SetFileName(string fileName);


}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class CompositeType
{
	bool boolValue = true;
	string stringValue = "Hello ";

	[DataMember]
	public bool BoolValue
	{
		get { return boolValue; }
		set { boolValue = value; }
	}

	[DataMember]
	public string StringValue
	{
		get { return stringValue; }
		set { stringValue = value; }
	}
}
