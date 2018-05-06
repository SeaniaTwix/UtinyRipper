using System.IO; 
using System.Text;
using UtinyRipper.Classes;

using Object = UtinyRipper.Classes.Object;

namespace UtinyRipper.AssetExporters
{
	public class BinaryAssetExporter : AssetExporter
	{
		public override IExportCollection CreateCollection(Object @object)
		{
			switch (@object.ClassID)
			{
				case ClassIDType.Texture2D:
				case ClassIDType.Cubemap:
					return new TextureExportCollection(this, (Texture2D)@object);

				default:
					return new AssetExportCollection(this, @object);
			}
		}

		public override bool Export(IAssetsExporter exporter, IExportCollection collection, string dirPath)
		{
			AssetExportCollection asset = (AssetExportCollection)collection;
			exporter.File = asset.Asset.File;

			string subFolder = asset.Asset.ClassID.ToString();
			string subPath = Path.Combine(dirPath, subFolder);
			string fileName = GetUniqueFileName(asset.Asset, subPath);
			string filePath = Path.Combine(subPath, fileName);

			if(!Directory.Exists(subPath))
			{
				Directory.CreateDirectory(subPath);
			}

			exporter.File = asset.Asset.File;
			using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
			{
				asset.Asset.ExportBinary(exporter, fileStream);
			}

			// Exporting file without encoded in here with .bytes extension
			if (asset.Asset.ClassIDName == "TextAsset") {
				byte[] bytes = ((TextAsset)asset.Asset).OriginalBytes;
				if (bytes != null)
				{
					FileStream fs = new FileStream(filePath + ".bytes", FileMode.CreateNew, FileAccess.Write);
					fs.Write(bytes, 0, bytes.Length);
					fs.Close();
				}
			}

			ExportMeta(exporter, asset, filePath);
			return true;
		}

		public override AssetType ToExportType(ClassIDType classID)
		{
			return AssetType.Meta;
		}
	}
}
