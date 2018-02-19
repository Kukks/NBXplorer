using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NBXplorer.Altcoins.Straks
{
	public class Networks
	{
		//Format visual studio
		//{({.*?}), (.*?)}
		//Tuple.Create(new byte[]$1, $2)
		static Tuple<byte[], int>[] pnSeed6_main = {
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x2d,0x4d,0xe3,0x9d}, 7575),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x2d,0x4c,0x58,0xf8}, 7575),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x6c,0x3d,0xd0,0xea}, 7575),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x68,0xee,0x9a,0xb0}, 7575),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x6c,0x3d,0xf1,0x41}, 7575),
	
};
		static Tuple<byte[], int>[] pnSeed6_test = {
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x2d,0x4d,0xe1,0x61}, 7565),
	Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x2d,0x20,0xc3,0x97}, 7565)
};

		public static void EnsureRegistered()
		{
			if(_LazyRegistered.IsValueCreated)
				return;
			// This will cause RegisterLazy to evaluate
			new Lazy<object>[] { _LazyRegistered }.Select(o => o.Value != null).ToList();
		}
		static Lazy<object> _LazyRegistered = new Lazy<object>(RegisterLazy, false);

		private static object RegisterLazy()
		{
			var port = 7575;
			NetworkBuilder builder = new NetworkBuilder();
			_Mainnet = builder.SetConsensus(new Consensus()
			{
				BIP34Hash = new uint256("00000b6321951f2ed170bbc9b7a360995176f2df418b0e275149bfce2fde3d6c"),
				PowLimit = new Target(new uint256("00000fffff000000000000000000000000000000000000000000000000000000")),
				PowTargetTimespan = TimeSpan.FromMinutes(30),
				PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
				PowAllowMinDifficultyBlocks = false,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 6048,
				MinerConfirmationWindow = 8064,
				CoinbaseMaturity = 100,
				HashGenesisBlock = new uint256("12a765e31ffd4059bada1e25190f6e98c99d9714d334efa41a195a7e7e04bfe2"),
				CoinType = 187,
				MinimumChainWork = new uint256("00000000000000000000000000000000000000000000000008b3d8d9dd5c81fd"),
				
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 63 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 5 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 204 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x88, 0xB2, 0x1E })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x88, 0xAD, 0xE4 })
			//.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("ltc"))
			//.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("ltc"))
			.SetMagic(0xdbb6c0fb)
			.SetPort(port)
			.SetRPCPort(7576)
			.SetName("stak-main")
			.AddAlias("stak-mainnet")
			.AddAlias("straks-mainnet")
			.AddAlias("straks-main")
			.AddDNSSeeds(new[]
			{
				new DNSSeedData("sm001.alphaqub.com", "sm001.alphaqub.com"),
				new DNSSeedData("sm002.alphaqub.com", "sm002.alphaqub.com"),
				new DNSSeedData("sm003.alphaqub.com", "sm003.alphaqub.com"),
				new DNSSeedData("sm004.alphaqub.com", "sm004.alphaqub.com"),
				new DNSSeedData("sm005.alphaqub.com", "sm005.alphaqub.com"),
			})
			.AddSeeds(ToSeed(pnSeed6_main))
			.SetGenesis(new Block(new BlockHeader()
				{
				BlockTime = DateTimeOffset.FromUnixTimeSeconds(1510790624),
					Nonce = 388287,
					Bits =  new Target(0x1e0ffff0),
					Version = 1,
					HashMerkleRoot = new uint256("15343d9e3cfff44854ec63cc588d5a1ed6ea971085c2be97acb0ea22c0e94fc4")

			}))
					.BuildAndRegister();

			//builder = new NetworkBuilder();
			//port = 19335;
			//_Testnet = builder.SetConsensus(new Consensus()
			//{
			//	MajorityEnforceBlockUpgrade = 51,
			//	MajorityRejectBlockOutdated = 75,
			//	MajorityWindow = 1000,
			//	PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
			//	PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
			//	PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
			//	PowAllowMinDifficultyBlocks = true,
			//	PowNoRetargeting = false,
			//	RuleChangeActivationThreshold = 1512,
			//	MinerConfirmationWindow = 2016,
			//	CoinbaseMaturity = 100,
			//	HashGenesisBlock = new uint256("4966625a4b2851d9fdee139e56211a0d88575f59ed816ff5e6a63deb4e3e29a0"),
			//})
			//.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			//.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 58 })
			//.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			//.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			//.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			//.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tltc"))
			//.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tltc"))
			//.SetMagic(0xf1c8d2fd)
			//.SetPort(port)
			//.SetRPCPort(19332)
			//.SetName("ltc-test")
			//.AddAlias("ltc-testnet")
			//.AddAlias("litecoin-test")
			//.AddAlias("litecoin-testnet")
			//.AddDNSSeeds(new[]
			//{
			//	new DNSSeedData("litecointools.com", "testnet-seed.litecointools.com"),
			//	new DNSSeedData("loshan.co.uk", "seed-b.litecoin.loshan.co.uk"),
			//	new DNSSeedData("thrasher.io", "dnsseed-testnet.thrasher.io"),
			//})
			//.AddSeeds(ToSeed(pnSeed6_test))
			//.SetGenesis(new Block(Encoders.Hex.DecodeData("010000000000000000000000000000000000000000000000000000000000000000000000d9ced4ed1130f7b7faad9be25323ffafa33232a17c3edf6cfd97bee6bafbdd97f60ba158f0ff0f1ee17904000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4804ffff001d0104404e592054696d65732030352f4f63742f32303131205374657665204a6f62732c204170706c65e280997320566973696f6e6172792c2044696573206174203536ffffffff0100f2052a010000004341040184710fa689ad5023690c80f3a49c8f13f8d45b8c857fbcbc8bc4a8e4d3eb4b10f4d4604fa08dce601aaf0f470216fe1b51850b4acf21b179c45070ac7b03a9ac00000000")))
			//.BuildAndRegister();

			//builder = new NetworkBuilder();
			//port = 19444;
			//_Regtest = builder.SetConsensus(new Consensus()
			//{
			//	SubsidyHalvingInterval = 150,
			//	MajorityEnforceBlockUpgrade = 51,
			//	MajorityRejectBlockOutdated = 75,
			//	MajorityWindow = 144,
			//	PowLimit = new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
			//	PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
			//	PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
			//	PowAllowMinDifficultyBlocks = true,
			//	MinimumChainWork = uint256.Zero,
			//	PowNoRetargeting = true,
			//	RuleChangeActivationThreshold = 108,
			//	MinerConfirmationWindow = 2016,
			//	CoinbaseMaturity = 100,
			//	HashGenesisBlock = new uint256("f5ae71e26c74beacc88382716aced69cddf3dffff24f384e1808905e0188f68f"),
			//})
			//.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			//.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 58 })
			//.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			//.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			//.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			//.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tltc"))
			//.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tltc"))
			//.SetMagic(0xdab5bffa)
			//.SetPort(port)
			//.SetRPCPort(19332)
			//.SetName("ltc-reg")
			//.AddAlias("ltc-regtest")
			//.AddAlias("litecoin-reg")
			//.AddAlias("litecoin-regtest")
			//.SetGenesis(new Block(Encoders.Hex.DecodeData("010000000000000000000000000000000000000000000000000000000000000000000000d9ced4ed1130f7b7faad9be25323ffafa33232a17c3edf6cfd97bee6bafbdd97dae5494dffff7f20000000000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4804ffff001d0104404e592054696d65732030352f4f63742f32303131205374657665204a6f62732c204170706c65e280997320566973696f6e6172792c2044696573206174203536ffffffff0100f2052a010000004341040184710fa689ad5023690c80f3a49c8f13f8d45b8c857fbcbc8bc4a8e4d3eb4b10f4d4604fa08dce601aaf0f470216fe1b51850b4acf21b179c45070ac7b03a9ac00000000")))
			//.BuildAndRegister();

			var home = Environment.GetEnvironmentVariable("HOME");
			var localAppData = Environment.GetEnvironmentVariable("APPDATA");

			if(string.IsNullOrEmpty(home) && string.IsNullOrEmpty(localAppData))
				return new object();

			if(!string.IsNullOrEmpty(home))
			{
				var bitcoinFolder = Path.Combine(home, ".straks");

				var mainnet = Path.Combine(bitcoinFolder, ".cookie");
				RPCClient.RegisterDefaultCookiePath(Networks._Mainnet, mainnet);

				//var testnet = Path.Combine(bitcoinFolder, "testnet4", ".cookie");
				//RPCClient.RegisterDefaultCookiePath(Networks._Testnet, testnet);

				//var regtest = Path.Combine(bitcoinFolder, "regtest", ".cookie");
				//RPCClient.RegisterDefaultCookiePath(Networks._Regtest, regtest);
			}
			else if(!string.IsNullOrEmpty(localAppData))
			{
				var bitcoinFolder = Path.Combine(localAppData, "Straks");

				var mainnet = Path.Combine(bitcoinFolder, ".cookie");
				RPCClient.RegisterDefaultCookiePath(Networks._Mainnet, mainnet);

				//var testnet = Path.Combine(bitcoinFolder, "testnet4", ".cookie");
				//RPCClient.RegisterDefaultCookiePath(Networks._Testnet, testnet);

				//var regtest = Path.Combine(bitcoinFolder, "regtest", ".cookie");
				//RPCClient.RegisterDefaultCookiePath(Networks._Regtest, regtest);
			}
			return new object();
		}


		private static IEnumerable<NetworkAddress> ToSeed(Tuple<byte[], int>[] tuples)
		{
			return tuples
					.Select(t => new NetworkAddress(new IPAddress(t.Item1), t.Item2))
					.ToArray();
		}

		private static Network _Mainnet;
		public static Network Mainnet
		{
			get
			{
				EnsureRegistered();
				return _Mainnet;
			}
		}

		//private static Network _Regtest;
		//public static Network Regtest
		//{
		//	get
		//	{
		//		EnsureRegistered();
		//		return _Regtest;
		//	}
		//}

		//private static Network _Testnet;
		//public static Network Testnet
		//{
		//	get
		//	{
		//		EnsureRegistered();
		//		return _Testnet;
		//	}
		//}
	}
}
