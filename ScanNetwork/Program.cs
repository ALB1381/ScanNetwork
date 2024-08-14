using ScanNetwork;
using System.Net.NetworkInformation;
using System.Threading.Tasks;


    string subnet = "192.168.1";
    await networkScanner.PingSweepAsync(subnet);


