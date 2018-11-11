using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Execview.BasketballAnalytics.Core.Domain;

namespace Execview.BasketballAnalytics.Core.Data
{
    public sealed class CsvDataProvider : IFileDataProvider
    {
        private readonly IPlayerParser _playerParser;

        public CsvDataProvider(IPlayerParser playerParser)
        {
            _playerParser = playerParser ?? throw new ArgumentNullException(nameof(playerParser));
        }

        public async Task<IReadOnlyList<Player>> GetPlayersAsync(string datafile)
        {
            if (string.IsNullOrWhiteSpace(datafile))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(datafile));

            // TODO: this should use Async API, but it is available only on .NET Core App 2.x and not .NET Standard 2.0
            var rows = File.ReadAllLines(datafile);
            var players = rows.Skip(1).Select(row => _playerParser.Build(row)).ToList();

            return await Task.FromResult(players);
        }
    }
}