﻿using pc2x.Paginacion.Core.DomainModels;
using pc2x.Paginacion.Core.RepositoryInterfaces;
using pc2x.Paginacion.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace pc2x.Paginacion.Service
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public IEnumerable<FacturaModel> GetAll(int pageSize, int page)
        {
            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page),
                    "El número de página debe ser mayor o igual a 1");

            return _facturaRepository.GetAll(pageSize, page);
        }

        public int Count()
        {
            return _facturaRepository.Count();
        }
    }
}
