using System.Threading.Channels;
using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Service;
using CodeCool.EhotelBuffet.Ui;

// ITimeService timeService = new TimeService();
 IMenuProvider menuProvider = new MenuProvider();
 IRefillService refillService = new RefillService();
// IGuestGroupProvider guestGroupProvider = null;
// IReservationProvider reservationProvider = null;
// IReservationManager reservationManager = null;
//
 IBuffetService buffetService = new BuffetService(menuProvider, refillService);
// IDiningSimulator diningSimulator =
//     new BreakfastSimulator(buffetService, reservationManager, guestGroupProvider, timeService);
//
// EhoteBuffetUi ui = new EhoteBuffetUi(reservationProvider, reservationManager, diningSimulator);
//
// ui.Run();
 IRefillStrategy basicRefillStrategy = new BasicRefillStrategy();
buffetService.Refill(basicRefillStrategy);
 
 DateTime date = DateTime.Now;
 DateTime newDate = date.AddMinutes(35);
 buffetService.Consume(MealType.Cereal);


 buffetService.Refill(basicRefillStrategy);
 