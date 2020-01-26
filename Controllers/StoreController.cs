using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
//using Store.Models;

namespace Store.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase {
        private static List<StoreItem> Store = new List<StoreItem> {
            new StoreItem {
                name = "CARs",
                description =" this is cars store "

            },
            new StoreItem {
                name = "Electronics",
                description = "a big electronics store"
            },
            new StoreItem {
                name = "Clothes",
                description = "rich clothes store "
            }
        };
        public StoreController () {

        }

        // GET api/store
        [HttpGet ()]
        public ActionResult<List<StoreItem>> Get () {
            return Ok(Store);
        }

        // GET api/store/5
        [HttpGet ("{name}")]
        public ActionResult<StoreItem> Get(string name) {
            var storeItem = Store.Find(item => 
            item.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (storeItem == null){
                return NotFound();
            }
            else
            {
                return Ok(storeItem);
            }
        }

        // POST api/store
        [HttpPost]
        public ActionResult Post(StoreItem storeItem)
        {
            var existingStoreItem = Store.Find(item =>
            item.name.Equals(storeItem.name, StringComparison.InvariantCultureIgnoreCase));
            if (existingStoreItem != null)
            {
                return Conflict("Cannot create !! the item already exits!!");
            }
            else
            {
                Store.Add(storeItem);
                var resourceURL = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(storeItem.name));
                return Created(resourceURL, storeItem);
            }
        }
        // PUT api/store/5
        [HttpPut]
        public ActionResult Put(StoreItem storeItem)
        {
            var existingStoreItem = Store.Find(item =>
            item.name.Equals(storeItem.name, StringComparison.InvariantCultureIgnoreCase));
            if (existingStoreItem == null )
            {
                return BadRequest("Cannot update a non existing item");
            }
            else
            {
                existingStoreItem.description = storeItem.description;
                return Ok();
            }
        }
        // DELETE api/store/5
        [HttpDelete ("{name}")]
        public ActionResult Delete(string name)
        {
            var storeItem = Store.Find(item =>
            item.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if(storeItem == null)
            {
                return NotFound();

            }
            else
            {
                Store.Remove(storeItem);
                return NoContent();
            }
        }
    }
}