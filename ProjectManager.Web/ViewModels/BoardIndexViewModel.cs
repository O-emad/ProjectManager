using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class BoardIndexViewModel
    {
        public BoardSectionCreateViewModel SectionCreateViewModel { get; set; }
        public List<BoardSectionModel> Sections { get; set; }

        public BoardIndexViewModel()
        {
            SectionCreateViewModel = new();
        }

        public BoardIndexViewModel(IEnumerable<BoardSectionModel> sections)
        {
            Sections = sections.ToList();
            SectionCreateViewModel = new();
        }
        public BoardIndexViewModel(BoardSectionCreateViewModel viewModel)
        {
            SectionCreateViewModel = viewModel;
        }

    }
}
