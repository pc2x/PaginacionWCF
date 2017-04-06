using Microsoft.VisualStudio.TestTools.UnitTesting;
using pc2x.Paginacion.WCF.Core.Contracts;
using System.ServiceModel;

namespace pc2x.Paginacion.Test
{
    [TestClass]
    public class WcfTest
    {
        private ChannelFactory<IFacturacionService> GetFactory()
        {
            return new ChannelFactory<IFacturacionService>(new WSHttpBinding(), "");
        }

        [TestMethod]
        public void WCF_FacturacionService_GetAll_NotNull()
        {
            var f = GetFactory();
            var proxy = f.CreateChannel();

            var x = proxy.ObtenerFacturas(20, 1);

            Assert.IsNotNull(x);

        }
    }
}
