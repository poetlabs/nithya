namespace canoodleapi.Repository
{// RouteRepository.cs
    using canoodleapi.DataObjects;
    using canoodleapi.Interfaces;
    using Dapper;
    public class RouteRepository : IRouteRepository
    {
        private readonly DapperContext _context;

        public RouteRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Routes>> GetAllRoutesAsync()
        {
            var query = "SELECT * FROM Routes";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Routes>(query);
        }

        public async Task<Routes> GetRouteByIdAsync(string routeId)
        {
            var query = "SELECT * FROM Routes WHERE RouteId = @RouteId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Routes>(query, new { RouteId = routeId });
        }

        public async Task CreateRouteAsync(Routes route)
        {
            var query = "INSERT INTO Routes (RouteId, RouteName) VALUES (@RouteId, @RouteName)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, route);
        }

        public async Task UpdateRouteAsync(Routes route)
        {
            var query = "UPDATE Routes SET RouteName = @RouteName WHERE RouteId = @RouteId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, route);
        }

        public async Task DeleteRouteAsync(string routeId)
        {
            var query = "DELETE FROM Routes WHERE RouteId = @RouteId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { RouteId = routeId });
        }
    }

}
