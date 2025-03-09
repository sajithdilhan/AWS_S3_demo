

using Amazon.Runtime;
using Amazon.S3;

var awsCredentials = new BasicAWSCredentials("", "");

var s3Client = new AmazonS3Client(awsCredentials, Amazon.RegionEndpoint.APSoutheast1);

CancellationTokenSource cts = new CancellationTokenSource(2000);

try
{
    var obj = await s3Client.GetObjectAsync("sajithd-demo-s3", "political compass.jpg", cts.Token);
    if (obj != null)
    {
        Console.WriteLine($"Retrieved {obj.Key} from bucket {obj.BucketName}");
        string destPath = Path.Combine(Directory.GetCurrentDirectory(), DateTime.UtcNow.ToString("dd_MM_yyyy_hh_mm_ss")+ ".jpg");
        using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        {
            obj.ResponseStream.CopyTo(fileStream);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
	throw;
}

