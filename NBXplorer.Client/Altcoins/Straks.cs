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
            if (_LazyRegistered.IsValueCreated)
                return;
            // This will cause RegisterLazy to evaluate
            new Lazy<object>[] { _LazyRegistered }.Select(o => o.Value != null).ToList();
        }
        static Lazy<object> _LazyRegistered = new Lazy<object>(RegisterLazy, false);


	    private static uint FromByteArray(byte[] bytes)
	    {

		  //  if (BitConverter.IsLittleEndian)
				//Array.Reverse(bytes);
			return BitConverter.ToUInt32(bytes, 0);
		}

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

			.SetMagic(FromByteArray(new byte[] { 0xb0, 0xd5, 0xf0, 0x2c }))
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
                Bits = new Target(0x1e0ffff0),
                Version = 1,
                HashMerkleRoot = new uint256("15343d9e3cfff44854ec63cc588d5a1ed6ea971085c2be97acb0ea22c0e94fc4")

            }))
            .BuildAndRegister();

            builder = new NetworkBuilder();
            port = 7575;
            _Testnet = builder.SetConsensus(new Consensus()
            {
                PowLimit = new Target(new uint256("00000fffff000000000000000000000000000000000000000000000000000000")),
                PowTargetTimespan = TimeSpan.FromSeconds(60),
                PowTargetSpacing = TimeSpan.FromSeconds(10),
                PowAllowMinDifficultyBlocks = true,
                PowNoRetargeting = false,
                RuleChangeActivationThreshold = 6480,
                MinerConfirmationWindow = 8640,
                CoinbaseMaturity = 100,
                HashGenesisBlock = new uint256("000000cd747bd0b653e1fe417b60c1d9e990600cf2ff193404ea12c3ecb348b4"),
                MinimumChainWork = new uint256("000000000000000000000000000000000000000000000000000000539862b0de"),
                BIP34Hash = new uint256("0000013dcc24cb29b041d5c89763f3aa44340faf556101783818a6ca8eb59e59"),
                CoinType = 187,

            })
            .SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 127 })
            .SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 19 })
            .SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
            .SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0xA2, 0xAE, 0xC9, 0xA6 })
            .SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x46, 0x00, 0x2A, 0x10 })
            // .SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tltc"))
            // .SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tltc"))
	            .SetMagic(FromByteArray(new byte[] { 0x2a, 0x1e, 0xd5, 0xd1 }))
			.SetPort(port)
            .SetRPCPort(7576)
            .SetName("stak-test")
            .AddAlias("stak-testnet")
            .AddAlias("straks-test")
            .AddAlias("straks-testnet")
            .AddDNSSeeds(new[]
            {
                new DNSSeedData("st001.radixpi.com", "st001.radixpi.com"),
                new DNSSeedData("st002.radixpi.com", "st002.radixpi.com"),
            })
            .AddSeeds(ToSeed(pnSeed6_test))
            .SetGenesis(new Block(new BlockHeader()
            {
                BlockTime = DateTimeOffset.FromUnixTimeSeconds(1510791350),
                Nonce = 1093629,
                Bits = new Target(0x1e0ffff0),
                Version = 1,
                HashMerkleRoot = new uint256("15343d9e3cfff44854ec63cc588d5a1ed6ea971085c2be97acb0ea22c0e94fc4")

            })).BuildAndRegister();

            builder = new NetworkBuilder();
            port = 19444;
            _Regtest = builder.SetConsensus(new Consensus()
            {
                PowLimit = new Target(new uint256("00000fffff000000000000000000000000000000000000000000000000000000")),
                PowTargetTimespan = TimeSpan.FromSeconds(60),
                PowTargetSpacing = TimeSpan.FromSeconds(1),
                PowAllowMinDifficultyBlocks = true,
                MinimumChainWork = uint256.Zero,
                PowNoRetargeting = true,
                RuleChangeActivationThreshold = 5400,
                MinerConfirmationWindow = 7200,
                CoinbaseMaturity = 100,
                BIP34Hash = new uint256("000002db0642636c786f756062dd7a92f35a2be1665816f8bfa33111ae902b37"),
                HashGenesisBlock = new uint256("000002db0642636c786f756062dd7a92f35a2be1665816f8bfa33111ae902b37"),
            })
            .SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 122 })
            .SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
            .SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
            .SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] {0xA2,0xAE,0xC9,0xA6 })
            .SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x46,0x00,0x2A,0x10})
			// .SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tltc"))
			// .SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tltc"))

	            .SetMagic(FromByteArray(new byte[] { 0x6e, 0x5c, 0x2c, 0xef }))
			.SetPort(port)
            .SetRPCPort(19332)
            .SetName("stak-reg")
            .AddAlias("stak-regtest")
            .AddAlias("straks-reg")
            .AddAlias("straks-regtest")
              .SetGenesis(new Block(new BlockHeader()
            {
                BlockTime = DateTimeOffset.FromUnixTimeSeconds(1510272549),
                Nonce = 1547336,
                Bits = new Target(0x1e0ffff0),
                Version = 1,
                HashMerkleRoot = new uint256("15343d9e3cfff44854ec63cc588d5a1ed6ea971085c2be97acb0ea22c0e94fc4")

            }))
			.BuildAndRegister();

            var home = Environment.GetEnvironmentVariable("HOME");
            var localAppData = Environment.GetEnvironmentVariable("APPDATA");

            if (string.IsNullOrEmpty(home) && string.IsNullOrEmpty(localAppData))
                return new object();

            if (!string.IsNullOrEmpty(home))
            {
                var bitcoinFolder = Path.Combine(home, ".straks");

                var mainnet = Path.Combine(bitcoinFolder, ".cookie");
                RPCClient.RegisterDefaultCookiePath(Networks._Mainnet, mainnet);

                var testnet = Path.Combine(bitcoinFolder, "testnet4", ".cookie");
                RPCClient.RegisterDefaultCookiePath(Networks._Testnet, testnet);

                var regtest = Path.Combine(bitcoinFolder, "regtest", ".cookie");
                RPCClient.RegisterDefaultCookiePath(Networks._Regtest, regtest);
            }
            else if (!string.IsNullOrEmpty(localAppData))
            {
                var bitcoinFolder = Path.Combine(localAppData, "Straks");

                var mainnet = Path.Combine(bitcoinFolder, ".cookie");
                RPCClient.RegisterDefaultCookiePath(Networks._Mainnet, mainnet);

                var testnet = Path.Combine(bitcoinFolder, "testnet4", ".cookie");
                RPCClient.RegisterDefaultCookiePath(Networks._Testnet, testnet);

                var regtest = Path.Combine(bitcoinFolder, "regtest", ".cookie");
                RPCClient.RegisterDefaultCookiePath(Networks._Regtest, regtest);
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

        private static Network _Regtest;
        public static Network Regtest
        {
            get
            {
                EnsureRegistered();
                return _Regtest;
            }
        }

        private static Network _Testnet;
        public static Network Testnet
        {
            get
            {
                EnsureRegistered();
                return _Testnet;
            }
        }
    }
}
