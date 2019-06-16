using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Alsahab.Setting.DTO.Models {
  public class PrefixDTO : BaseDTO//<PrefixDTO, Prefix>
   {
    public String Title { get; set; }
    public bool? IsDefault { get; set; }
  }

  public class PrefixValidator : AbstractValidator<PrefixDTO> {

    public PrefixValidator () {
      RuleFor (x => x.Title).NotEmpty ();
      RuleFor (x => x.IsDefault).NotEmpty ();
      RuleFor (x => x.IsDeleted).NotEqual (true);
    }

  }

}