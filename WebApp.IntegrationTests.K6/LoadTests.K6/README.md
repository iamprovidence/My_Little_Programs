# LoadTests.K6

# Run

* install K6 by [link](https://k6.io/docs/getting-started/installation/)
* execute `start-server.ps1`
* execute `run.ps1`

## Load tests

### Stress testing

Stress Testing is a type of load testing used to determine the limits of the system. 

The purpose of this test is to verify the stability and reliability of the system under extreme conditions.

You typically want to stress test an API or website to:

* determine how your system will behave under extreme conditions.
* determine what is the maximum capacity of your system in terms of users or throughput.
* determine the breaking point of your system and its failure mode.
* determine if your system will recover without manual intervention after the stress test is over.

<p align="center">
    <img alt="Stress test" src="https://raw.githubusercontent.com/iamprovidence/WebAppTests/master/docs/images/load-testing/stress-test.png" />
</p>

### Spike testing

Spike testing is a type of stress testing that immediately overwhelms the system with an extreme surge of load.

You want to execute a spike test to:

* determine how your system will perform under a sudden surge of traffic.
* determine if your system will recover once the traffic has subsided.

<p align="center">
    <img alt="Spike test" src="https://raw.githubusercontent.com/iamprovidence/WebAppTests/master/docs/images/load-testing/spike-test.png" />
</p>

### Load testing

Load Testing is a type of Performance Testing used to determine a system's behavior under both normal and peak conditions.

Load Testing is used to ensure that the application performs satisfactorily when many users access it at the same time.

You should run Load Test to:

* assess the current performance of your system under typical and peak load.
* make sure you are continuously meeting the performance standards as you make changes to your system (code and infrastructure).

<p align="center">
    <img alt="Load test" src="https://raw.githubusercontent.com/iamprovidence/WebAppTests/master/docs/images/load-testing/load-test.png" />
</p>

### Soak testing

The soak test uncovers performance and reliability issues stemming from a system being under pressure for an extended period.

You typically run this test to:

* verify that your system doesn't suffer from bugs or memory leaks, which result in a crash or restart after several hours of operation.
* verify that expected application restarts don't lose requests.
* find bugs related to race-conditions that appear sporadically.
* make sure your database doesn't exhaust the allotted storage space and stops.
* make sure your logs don't exhaust the allotted disk storage.
* make sure the external services you depend on don't stop working after a certain amount of requests are executed.

<p align="center">
    <img alt="Soak test" src="https://raw.githubusercontent.com/iamprovidence/WebAppTests/master/docs/images/load-testing/soak-test.png" />
</p>
