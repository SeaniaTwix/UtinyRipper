using System;
using System.IO;
using System.Text;
using UtinyRipper.AssetExporters;
using UtinyRipper.Exporter.YAML;

namespace UtinyRipper.Classes
{
	public class TextAsset : NamedObject
	{
		public TextAsset(AssetInfo assetInfo):
			base(assetInfo)
		{
		}

		/// <summary>
		/// Less than 2017.1
		/// </summary>
		public static bool IsReadPath(Version version)
		{
			return version.IsLess(2017);
		}

		public override void Read(AssetStream stream)
		{
			base.Read(stream);

			Script = stream.ReadStringAligned();
			if(IsReadPath(stream.Version))
			{
				PathName = stream.ReadStringAligned();
			}
			
			OriginalBytes = stream.ob;
		}

		public override void ExportBinary(IAssetsExporter exporter, Stream stream)
		{
			using (StreamWriter writer = new StreamWriter(stream))
			{
				writer.Write(Script);
			}
		}

		protected void ReadBase(AssetStream stream)
		{
			base.Read(stream);
		}

		protected sealed override YAMLMappingNode ExportYAMLRoot(IAssetsExporter exporter)
		{
			throw new NotSupportedException();
		}
		
		public override string ExportExtension => "txt";

		public byte[] OriginalBytes;
		public string Script { get; private set; } = string.Empty;
		public string PathName { get; private set; } = string.Empty;
	}
}
