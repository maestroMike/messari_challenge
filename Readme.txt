Solution for challenge from Messari
https://resonant-zipper-d74.notion.site/Messari-Market-Data-Coding-Challenge-rev-28-Feb-2022-e513357eaeb34b9a9ab9805af37d96b0
Objective: Build an efficient tool to compute aggregate market data from raw trades.

To compile you need to install .Net 6 sdk https://dotnet.microsoft.com/en-us/download/dotnet/6.0 https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu
Run following command on solution root directory to get binary in 'bin' folder:
dotnet publish MarketDataParser -c Release -o bin

To run unit tests:
dotnet test MarketDataParser.Tests

Performance testing results:
Intel(R) Core(TM) i5-4570 CPU @ 3.20GHz 4 cores
Windows WSL - Ubuntu 20.04

time (./stdoutinator_amd64_linux.bin > /dev/null)

real    0m17.207s
user    0m16.295s
sys     0m3.219s

time (./stdoutinator_amd64_linux.bin | ./MarketDataParser > /tmp/MarketDataParse_out.txt)

real    0m21.678s
user    1m0.995s
sys     0m16.486s
