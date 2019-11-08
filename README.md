# FibUhr

The time can be read as follows:
* The squares are assigned a weighting according to their side length.
* the page lengths are 1,1,2,3 and 5
* Since this is the beginning of the Fibonacci episode, this is also the reason for their name
* Now the decimal numbers bon 1 to 12 can be displayed
* the times are for a.m. and p.m are the same

Now one can get the time by summing up the valence of the squares. The colors mean

* RED: sum of hours
* Green: sum of minutes * 5
* Blue: sum of both
* White: unused

The time changes every 5 minutes.
Some times can be displayed in different ways. The combination of the representations are as follows

combinations:
  *  0 := {Ø}
  *  1 := {(1); (1 *)}
  *  2 := {(2); (1,1 *)}
  *  3 := {(3); (2,1); (2,1 *)}
  *  4 := {(1,1 *, 2); (3,1); (3,1 *)}
  *  5 := {(1,1 *, 3), (2,3), (5)}
  *  6 := {(5,1) (5,1 *); (3,2,1), (3,2,1 *)}
  *  7 := {(5,2); (5,1,1 *)}
  *  8 := {(5,3); (5,2,1); (5,2,1 *)}
  *  9 := {(5,3,1); (5,3,1 *); (5,2,1,1 *)}
  * 10 := {(5,3,2); (5,3,1,1 *)}
  * 11 := {(5,3,2,1), (5,3,2,1 *)}

The combinations change every 0.2 seconds.
