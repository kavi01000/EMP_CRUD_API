using AutoMapper;
using EmpList.Model;
using EmpList.ModelDTO;
using EmpList.Repository;
using EmpList.UnitOfWork;

namespace EmpList.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unit;

        public ContactService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<ContactDto> CreateAsync(ContactDto dto)
        {
            var contact = new Contact
            {
                EmployeeId = dto.EmployeeId,
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                PinCode = dto.PinCode,
                Age = dto.Age,
                DOB = dto.DOB,
                CreatedBy = dto.CreatedBy
            };

            var saved = await _unit.Contacts.AddAsync(contact);

            return new ContactDto
            {
                ContactId = saved.ContactId,
                EmployeeId = saved.EmployeeId,
                Email = saved.Email,
                PhoneNo = saved.PhoneNo,
                PinCode = saved.PinCode,
                Age = saved.Age,
                DOB = saved.DOB,
                IsActive = saved.IsActive
            };
        }

        public async Task<ContactDto> UpdateAsync(long id, ContactDto dto)
        {
            var contact = await _unit.Contacts.GetByIdAsync(id);

            if (contact == null)
                throw new Exception("Contact not found");

            contact.Email = dto.Email;
            contact.PhoneNo = dto.PhoneNo;
            contact.PinCode = dto.PinCode;
            contact.Age = dto.Age;
            contact.DOB = dto.DOB;
            contact.ModifiedBy = dto.ModifiedBy;

            await _unit.Contacts.UpdateAsync(contact);

            return new ContactDto
            {
                ContactId = contact.ContactId,
                EmployeeId = contact.EmployeeId,
                Email = contact.Email,
                PhoneNo = contact.PhoneNo,
                PinCode = contact.PinCode,
                Age = contact.Age,
                DOB = contact.DOB,
                IsActive = contact.IsActive,
                ModifiedBy = contact.ModifiedBy,
                ModifiedOn = contact.ModifiedOn
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await _unit.Contacts.DeleteAsync(id);
            return true;
        }

        public async Task<ContactDto?> GetByIdAsync(long id)
        {
            var contact = await _unit.Contacts.GetByIdAsync(id);

            if (contact == null) return null;

            return new ContactDto
            {
                ContactId = contact.ContactId,
                EmployeeId = contact.EmployeeId,
                Email = contact.Email,
                PhoneNo = contact.PhoneNo,
                PinCode = contact.PinCode,
                Age = contact.Age,
                DOB = contact.DOB,
                IsActive = contact.IsActive
            };
        }

        public async Task<IEnumerable<ContactDto>> GetAllAsync()
        {
            var list = await _unit.Contacts.GetAllAsync();

            return list.Select(c => new ContactDto
            {
                ContactId = c.ContactId,
                EmployeeId = c.EmployeeId,
                Email = c.Email,
                PhoneNo = c.PhoneNo,
                PinCode = c.PinCode,
                Age = c.Age,
                DOB = c.DOB,
                IsActive = c.IsActive
            });
        }
    }

}
