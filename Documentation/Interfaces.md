# Interfaces
## Description of available interfaces

### Basic Control
- Name: ```IBasicControl```  
  Basic Functionality to set/get modes, frequency, PTT etc.
  - ```SetMode```
  - ```GetMode```
  - ```SetFrequency```
  - ```GetFrequency```
  - ```SetPTT```
  - ```GetPTT```

- Name: ```IExtendedControl```
  Functionality to manage VFOs, memory, repeater offsets etc.
  - ```SelectVFO```
  - ```GetVFO```
  - ```ToggleVFO```
  - ```ToggleVM```: Toggles between VFO and memory mode. When switching to VFO mode the last selected VFO is used