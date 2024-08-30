using ServiceStack;
using Xunit;

namespace TemplateDomain.WebApi.IntegrationTests;

public class RSAKeysFactoryTests
{
    [Fact]
    public void ShouldCreatePrivateAndPublicRSA2048Keys()
    {
        var privateKey = RsaUtils.CreatePrivateKeyParams(RsaKeyLengths.Bit2048);
        var publicKey = privateKey.ToPublicRsaParameters();
        var privateKeyXml = privateKey.ToPrivateKeyXml();
        var publicKeyXml = privateKey.ToPublicKeyXml();
    }
}