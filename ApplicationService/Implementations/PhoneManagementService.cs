using ApplicationService.DTOs;
using Data.Context;
using Data.Entitites;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Implementations
{
    public class PhoneManagementService
    {
        public List<PhoneDTO> Get(string query)
        {
            List<PhoneDTO> phonesDto = new List<PhoneDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (query == null)
                {
                    foreach (var item in unitOfWork.PhoneRepository.Get())
                    {
                        phonesDto.Add(new PhoneDTO
                        {
                            Id = item.Id,
                            PhoneName = item.PhoneName,
                            Description = item.Description,
                            Release = item.Release,
                            Price = item.Price,
                            Image = item.Image,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }                           
                        });
                    }
                }
                else
                {
                    foreach (var item in unitOfWork.PhoneRepository.GetByQuery().Where(c => c.PhoneName.Contains(query)).ToList())
                    {
                        phonesDto.Add(new PhoneDTO
                        {
                            Id = item.Id,
                            PhoneName = item.PhoneName,
                            Description = item.Description,
                            Release = item.Release,
                            Price = item.Price,
                            Image = item.Image,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }
                        });
                    }
                }
            }
            return phonesDto;
        }

        public PhoneDTO GetById(long id)
        {
            PhoneDTO phoneDto = new PhoneDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Phone phone = unitOfWork.PhoneRepository.GetByID(id);
               
                phoneDto = new PhoneDTO
                {
                    Id = phone.Id,
                    PhoneName = phone.PhoneName,
                    Description = phone.Description,
                    Release = phone.Release,
                    Price = phone.Price,
                    Image = phone.Image,
                    Category = new CategoryDTO
                    {
                        Id = phone.Category.Id,
                        Title = phone.Category.Title,
                        Description = phone.Category.Description
                    }
                };
            }
            return phoneDto;
        }

        public bool Save(PhoneDTO phoneDto)
        {
            Phone phone = new Phone()
            {
                Id = phoneDto.Id,
                PhoneName = phoneDto.PhoneName,
                Description = phoneDto.Description,
                Release = phoneDto.Release,
                Price = phoneDto.Price,
                Image = phoneDto.Image,
                CategoryId = phoneDto.Category.Id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (phoneDto.Id == 0)
                    {
                        unitOfWork.PhoneRepository.Insert(phone);
                    }
                    else
                    {
                        unitOfWork.PhoneRepository.Update(phone);
                    }
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                System.Console.WriteLine(phone);
                return false;
            }
        }



        public bool Delete(long id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Phone phone = unitOfWork.PhoneRepository.GetByID(id);
                    unitOfWork.PhoneRepository.Delete(phone);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
