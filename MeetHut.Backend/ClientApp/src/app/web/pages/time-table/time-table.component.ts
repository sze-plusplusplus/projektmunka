import { Component, OnInit} from '@angular/core';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { addDays, addHours, endOfMonth, isSameDay, isSameMonth, startOfDay, subDays } from 'date-fns';
import { Subject } from 'rxjs';
import { RoomCalendarDTO } from '../../models';
import { Router } from '@angular/router';
import { RoomService } from '../../services';

interface IEventColor {
  primary: string;
  secondary: string;
}

const colors: Record<string, IEventColor> = {
  owner: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  ownerLocked: {
    primary: '#ad6c21',
    secondary: '#faf3e3',
  },
  locked: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  },
  general: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
};

// FOR TEST
/*const rooms: RoomCalendarDTO[] = [
  new RoomCalendarDTO(1, 'Event 1', 'asf-asds', subDays(startOfDay(new Date()), 1), addDays(new Date(), 1), true, false),
  new RoomCalendarDTO(2, 'Event 2', 'asf-asd12sds', startOfDay(new Date()), undefined, true, true),
  new RoomCalendarDTO(3, 'Event 3', 'asf-aasd23d12sds', addHours(startOfDay(new Date()), 2), addHours(new Date(), 2), false, false),
  new RoomCalendarDTO(4, 'Event 4', 'asf-asdds0asd', subDays(endOfMonth(new Date()), 3), addDays(endOfMonth(new Date()), 3), false, true)
];*/

@Component({
  selector: 'app-time-table',
  templateUrl: './time-table.component.html',
  styleUrls: ['./time-table.component.scss']
})
export class TimeTableComponent implements OnInit {
  // eslint-disable-next-line @typescript-eslint/naming-convention
  CalendarView = CalendarView;

  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  refresh: Subject<any> = new Subject();

  roomEvents: RoomCalendarDTO[] = [];
  events: CalendarEvent[] = [];

  activeDayIsOpen = false;

  constructor(private router: Router, private roomService: RoomService) {
  }

  ngOnInit(): void {
    this.getRooms();
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      this.activeDayIsOpen = !((isSameDay(this.viewDate, date) && this.activeDayIsOpen) ||
        events.length === 0);
      this.viewDate = date;
    }
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }

  roomClicked(event: CalendarEvent): void {
    const room = this.roomEvents.find(x => x.id === event.id);

    if (room) {
      this.router.navigate(['room', room.publicId]);
    }
  }

  private getRooms(): void {
    this.roomService.getCalendar().then((res) => {
      this.roomEvents = res;
      this.events = res.map(r => ({
        id: r.id,
        start: r.startTime,
        end: r.endTime,
        title: r.name,
        color: this.getColor(r.isLocked, r.isOwner)
      } as CalendarEvent));

      if (!this.activeDayIsOpen) {
        if (res.some(x => isSameDay(x.startTime, new Date()))) {
          this.activeDayIsOpen = true;
        }
      }
    }).catch(() => {
      this.roomEvents = [];
      this.events = [];
      this.activeDayIsOpen = false;
    });
  }

  private getColor(isLocked: boolean, isOwner: boolean): IEventColor {
    if (isOwner) {
      if (isLocked) {
        return colors.ownerLocked;
      }

      return colors.owner;
    }

    if (isLocked) {
      return colors.locked;
    }

    return colors.general;
  }
}
