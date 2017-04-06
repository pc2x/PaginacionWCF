using Microsoft.VisualStudio.TestTools.UnitTesting;
using pc2x.Paginacion.Repository;

namespace pc2x.Paginacion.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void REPO_FacturaRepository_GetAll_NotNull()
        {
            var repo = new FacturaRepository();
            var x = repo.GetAll(300,1);

            Assert.IsNotNull(x);
        }
    }
}
