﻿using System.Collections.Generic;
using UtinyRipper.AssetExporters;
using UtinyRipper.Exporter.YAML;
using UtinyRipper.SerializedFiles;

namespace UtinyRipper.Classes.LightmapSettingss
{
	public struct EnlightenRendererInformation : IAssetReadable, IYAMLExportable
	{
		/// <summary>
		/// Not Release
		/// </summary>
		public static bool IsReadGeometryHash(TransferInstructionFlags flags)
		{
			return !flags.IsSerializeGameRelease();
		}

		public void Read(AssetStream stream)
		{
			Renderer.Read(stream);
			DynamicLightmapSTInSystem.Read(stream);
			SystemId = stream.ReadInt32();
			InstanceHash.Read(stream);
			if(IsReadGeometryHash(stream.Flags))
			{
				GeometryHash.Read(stream);
			}
		}

		public IEnumerable<Object> FetchDependencies(ISerializedFile file, bool isLog = false)
		{
			yield return Renderer.FetchDependency(file, isLog, () => nameof(EnlightenRendererInformation), "renderer");
		}

		public YAMLNode ExportYAML(IAssetsExporter exporter)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("renderer", Renderer.ExportYAML(exporter));
			node.Add("dynamicLightmapSTInSystem", DynamicLightmapSTInSystem.ExportYAML(exporter));
			node.Add("systemId", SystemId);
			node.Add("instanceHash", InstanceHash.ExportYAML(exporter));
			node.Add("geometryHash", GeometryHash.ExportYAML(exporter));
			return node;
		}

		public int SystemId { get; private set; }

		public PPtr<Object> Renderer;
		public Vector4f DynamicLightmapSTInSystem;
		public Hash128 InstanceHash;
		public Hash128 GeometryHash;
	}
}
