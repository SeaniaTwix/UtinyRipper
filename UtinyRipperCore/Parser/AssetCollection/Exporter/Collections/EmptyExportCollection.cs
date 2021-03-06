﻿using System;
using System.Collections.Generic;
using UtinyRipper.AssetExporters.Classes;
using UtinyRipper.Classes;

using Object = UtinyRipper.Classes.Object;

namespace UtinyRipper.AssetExporters
{
	internal sealed class EmptyExportCollection : IExportCollection
	{
		public EmptyExportCollection(DummyAssetExporter assetExporter, string name)
		{
			if (assetExporter == null)
			{
				throw new ArgumentNullException(nameof(assetExporter));
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name));
			}
			AssetExporter = assetExporter;
			Name = name;
		}

		public bool IsContains(Object @object)
		{
			return false;
		}

		public string GetExportID(Object @object)
		{
			throw new NotSupportedException();
		}

		public ExportPointer CreateExportPointer(Object @object, bool isLocal)
		{
			throw new NotSupportedException();
		}

		public IAssetExporter AssetExporter { get; }
		public IEnumerable<Object> Objects
		{
			get { yield break; }
		}
		public string Name { get; }
		public UtinyGUID GUID => throw new NotSupportedException();
		public IYAMLExportable MetaImporter => throw new NotSupportedException();
	}
}
