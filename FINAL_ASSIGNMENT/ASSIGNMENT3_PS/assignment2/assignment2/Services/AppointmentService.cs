using assignment2.Data;
using assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace assignment2.Services
{

    public class AppointmentService : IAppointmentService
    {
        private IMongoRepository<Appointment> _repository;
        private global::Moq.Mock<IMongoRepository<Appointment>> repoMock;

        public AppointmentService(IMongoRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public AppointmentService(global::Moq.Mock<IMongoRepository<Appointment>> repoMock)
        {
            this.repoMock = repoMock;
        }

        public bool Add(Appointment appointment)
        {
            bool alreadyExists = AppointmentExistsWithDate(appointment);

            if (alreadyExists == false)
            {
                _repository.Create(appointment);
                //unitOfWork.Save();
            }
            else
            {
                return false;
            }
            return true;
        }
        public void Update(Appointment appointment)
        {
            _repository.Update(appointment);
            //unitOfWork.Save();
        }
        public void Delete(string id)
        {
           _repository.Delete(id);
            //unitOfWork.Save();
        }
        public bool AppointmentExists(string id)
        {
            return _repository.Get().Any(e => e.BsonID == id);
        }
        public bool AppointmentExistsWithDate(Appointment appointment)
        {
            return _repository.Get().Any(a => a.date == appointment.date); 
        }

        public IEnumerable<Appointment> Get(DateTime d1, DateTime d2)
        { 
            return _repository.Get().Where(entry => entry.date >= d1 && entry.date <= d2);
        }

        public IEnumerable<Appointment> Search(string searchString)
        {
            var appointments = _repository.Get().Where(a => a.clientName.Contains(searchString));
            return appointments;
        }
        public IEnumerable<Appointment> Get()
        {
            var appointments = _repository.Get();
            return appointments;
        }
        public Appointment Get(string id)
        { 
            var appointment = _repository.GetById(id);
            return appointment;
        }

        public Document Export(int type)
        {
            List<Appointment> lstData = _repository.Get().ToList();
            return ExporterFactory.Instance().CreateExporter(type).export(lstData);
        }

        public Document ExportDate(IEnumerable<Appointment> applist, int type)
        {
            return ExporterFactory.Instance().CreateExporter(type).export(applist.ToList());
        }

        public string GetAndConvertToJson()
        {
            IEnumerable<Appointment> applist = _repository.Get();

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                Formatting = Formatting.Indented

            };
            return JsonConvert.SerializeObject(applist, microsoftDateFormatSettings);

        }
        public bool Duplicate(Appointment appointment)
        {
            if(_repository.DuplicateBsonId(appointment) == true)
            {
                _repository.Delete(appointment);
                return true;
            }
            return false;      
        }
    }

}
