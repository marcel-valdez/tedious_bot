This project contains generator tools I found to be necessary as I progress as a software developer, in order to automate tedious tasks.  
  
Current generators:  
  
**CArrayGenerator**: Generates C Arrays.  
    Usage: CArrayGenerator.exe int [3][3]  
  
**Watch**: Periodically executes a specified command.  
    Usage: watch.exe -f [frequency in seconds] -n [max number of executions] [command]  
  
**Formatter**: Replace a regex match with a String.Format, optionally removing non-captures.  
    Usage: formatter.exe Usage:\nformatter [filename] -pattern [regex] -format [format] -verbose -only-capture