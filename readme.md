# .NET Test

The file measurements.json in the data directory contains a list of measurements on devices.

- Devices can use or produce energy (in/out). 
- Devices are grouped in two device groups: group_a and group_b.

Requirements:
1) Read the measurements.json file and store the data in a Dictionary with key:
    - resourceId
    - deviceName
    - deviceGroup
    - direction
2) Implement the "api/groups" endpoint that outputs the totals for both groups for in and outgoing power.
    - group, direction, power
    - The power total should have 4 decimal digits
    - Add unit tests
3) Implement the "api/devices" endpoint that outputs a list of all devices, and their max power, ordered by group, direction and power(ascending):
    - Device: deviceId, group, direction, power.max
    - The deviceId must be the UUID without '-' 
    - The max power must have 4 decimal digits
    - Add unit tests

Ensure your code can withstand changing requirements.