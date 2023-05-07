using Online_Pharmacy__Server.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Pharmacy__Server.DTOMappers
{
    public class UserMapper
    {
        // input is User in folder Models
        public UserDTO ToDTO(UserDTO user)
        {
            return null;
        }

        // Object is User in folder Models
        public Object ToObject(UserDTO dto)
        {
            return null;
        }
    }

    public class GroupMapper
    {
        public GroupDTO ToDTO(GroupDTO obj)
        {
            GroupDTO dto = new GroupDTO();
            /*dto.Id = obj.;
            dto.Name = obj.;
            dto.Description = obj.;
            dto.Status = obj.;
            dto.HasFunctions = obj.;*/
            return dto;
        }

        public Object ToObject(GroupDTO dto)
        {
            /*Group group = new Group();
            group. = dto.Id;
            group. = dto.Name;
            group. = dto.Description;
            group. = dto.Status;
            group. = dto.HasFunctions;
            return group;*/
            return null;
        }
    }


    // T is Function in folder Models
    public class FunctionMapper
    {
        public FunctionDTO ToDTO(FunctionDTO obj)
        {
            FunctionDTO dto = new FunctionDTO();
            /*dto.Id = obj.;
            dto.ShortDescription = obj.;
            dto.FullDescription = obj.;
            dto.Status = obj.;*/
            return dto;
        }

        public Object ToObject(FunctionDTO dto)
        {
            /*Function func = new Function();
            func. = dto.Id;
            func. = dto.ShortDescription;
            func. = dto.FullDescription;
            func. = dto.Status;
            return func;*/
            return null;
        }
    }

}