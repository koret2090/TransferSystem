# Apache Benchmark
## Доступ напрямую
```
[mrskl1f@thinkpad ~]$ ab -c 10 -n 10000 https://localhost:5002/api/v1/teams/5
This is ApacheBench, Version 2.3 <$Revision: 1879490 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            5002
SSL/TLS Protocol:       TLSv1.2,ECDHE-RSA-AES256-GCM-SHA384,2048,256
Server Temp Key:        X25519 253 bits
TLS Server Name:        localhost

Document Path:          /api/v1/teams/5
Document Length:        167 bytes

Concurrency Level:      10
Time taken for tests:   23.219 seconds
Complete requests:      10000
Failed requests:        0
Total transferred:      3270000 bytes
HTML transferred:       1670000 bytes
Requests per second:    430.69 [#/sec] (mean)
Time per request:       23.219 [ms] (mean)
Time per request:       2.322 [ms] (mean, across all concurrent requests)
Transfer rate:          137.53 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        2    8   3.4      7      86
Processing:     3   12  37.2     10    3663
Waiting:        0   11  36.8      9    3634
Total:          6   20  38.0     18    3733

Percentage of the requests served within a certain time (ms)
  50%     18
  66%     21
  75%     23
  80%     24
  90%     29
  95%     34
  98%     41
  99%     45
 100%   3733 (longest request)
```
## Доступ при кеше и 3 портах
```
[mrskl1f@thinkpad ~]$ ab -c 10 -n 5000 http://localhost/api/v1/teams/5
This is ApacheBench, Version 2.3 <$Revision: 1879490 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 500 requests
Completed 1000 requests
Completed 1500 requests
Completed 2000 requests
Completed 2500 requests
Completed 3000 requests
Completed 3500 requests
Completed 4000 requests
Completed 4500 requests
Completed 5000 requests
Finished 5000 requests


Server Software:        TransferSystem
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/teams/5
Document Length:        157 bytes

Concurrency Level:      10
Time taken for tests:   0.495 seconds
Complete requests:      5000
Failed requests:        0
Non-2xx responses:      5000
Total transferred:      1555000 bytes
HTML transferred:       785000 bytes
Requests per second:    10107.01 [#/sec] (mean)
Time per request:       0.989 [ms] (mean)
Time per request:       0.099 [ms] (mean, across all concurrent requests)
Transfer rate:          3069.61 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.1      0       1
Processing:     0    1   0.3      1       7
Waiting:        0    1   0.3      0       7
Total:          0    1   0.3      1       7
ERROR: The median and mean for the waiting time are more than twice the standard
       deviation apart. These results are NOT reliable.

Percentage of the requests served within a certain time (ms)
  50%      1
  66%      1
  75%      1
  80%      1
  90%      1
  95%      1
  98%      2
  99%      2
 100%      7 (longest request)

```