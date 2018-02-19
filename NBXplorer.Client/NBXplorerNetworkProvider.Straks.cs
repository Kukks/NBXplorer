using System;
using System.Collections.Generic;
using System.Text;

namespace NBXplorer
{


	public partial class NBXplorerNetworkProvider
	{
		private void InitStraks(ChainType chainType)
		{
			NBXplorer.Altcoins.Straks.Networks.EnsureRegistered();
			Add(new NBXplorerNetwork()
			{
				
				CryptoCode = "STAK",
				MinRPCVersion = 140200,
				NBitcoinNetwork = chainType == ChainType.Main ? NBXplorer.Altcoins.Straks.Networks.Mainnet : throw new NotSupportedException(chainType.ToString()),
				DefaultSettings = NBXplorerDefaultSettings.GetDefaultSettings(chainType)
			});
		}

		public NBXplorerNetwork getSTAK()
		{
			return GetFromCryptoCode("STAK");
		}
	}
}
