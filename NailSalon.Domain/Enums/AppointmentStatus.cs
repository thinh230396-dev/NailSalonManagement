using System;
using System.Collections.Generic;
using System.Text;

namespace NailSalon.Domain.Enums;

public enum AppointmentStatus
{
    Pending = 0, // chờ duyệt 
    Confirmed = 1, // đã xác nhận
    Completed = 2, // đã hoàn thành
    Cancelled = 3 
}
