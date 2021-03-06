﻿using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("Orders")]
    public class OrdersController : GenericController<Order, int>
    {

        public OrdersController(NorthwindDbContext db) : base(db)
        {
        }

        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.OrderDetails.Where(x => x.OrderId == key);
        }

        [EnableQuery]
        public SingleResult<Shipper> GetShipVia([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.ShipVia));
        }

        [EnableQuery]
        public SingleResult<Customer> GetCustomer([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.Customer));
        }

        [EnableQuery]
        public SingleResult<Employee> GetEmployee([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.Employee));
        }
    }
}