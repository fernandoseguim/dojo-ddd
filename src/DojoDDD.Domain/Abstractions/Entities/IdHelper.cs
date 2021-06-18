using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DojoDDD.Domain.Abstractions.Entities
{
    public static class IdHelper
    {
        public static string Load(params object[] values)
        {
            var index = values.Aggregate(string.Empty, (current, value) => current + $"{value}/");
            return index.TrimEnd('/');
        }

        public static string LoadForClients(string id) => Load("clients", id);
        public static string LoadForProducts(string id) => Load("products", id);
        public static string LoadForOrders(string id) => Load("orders", id);

        public static string RemoveClientsPrefix(string id) => id.Replace("clients/", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        public static string RemoveProductsPrefix(string id) => id.Replace("products/", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        public static string RemoveOrdersPrefix(string id) => id.Replace("orders/", string.Empty, StringComparison.InvariantCultureIgnoreCase);

        public static string AsSha256(string value)
        {
            using var sha256Hash = SHA256.Create();

            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            var builder = new StringBuilder();
            foreach(var item in bytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}