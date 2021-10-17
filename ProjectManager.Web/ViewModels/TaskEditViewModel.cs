using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class TaskEditViewModel
    {
        public Guid TaskProjectId { get; set; }

        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Team name must be within a minimum length 2 and maximum length 50")]
        public string Name { get; set; }
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name="Completion Status")]
        public bool CompletionStatus { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Keep description in 500 charachter limit")]
        public string Description { get; set; }

        [Display(Name = "Assignee")]
        public Guid UserId { get; set; }
        public SelectList TeamMembers { get; set; }

        [Display(Name = "Section")]
        public Guid Section { get; set; }

        public SelectList Sections { get; set; }

        [Display(Name = "Main Section")]
        public Guid BoardSection { get; set; }

        public SelectList BoardSections { get; set; }

        public void Generate(IEnumerable<Member> members, IEnumerable<SectionModel> sections,
             IEnumerable<BoardSectionModel> boardSections)
        {
            TeamMembers = new SelectList(members, "Id", "UserName", new { UserId });
            Sections = new SelectList(sections, "Id", "Name", new { Section });
            BoardSections = new SelectList(boardSections, "Id", "Name", new { BoardSection });
        }

        public TaskEditViewModel(TaskModel task,IEnumerable<Member> members, IEnumerable<SectionModel> sections,
             IEnumerable<BoardSectionModel> boardSections, Guid projectId)
        {
            TaskProjectId = projectId;
            Name = task.Name;
            Id = task.Id;
            Description = task.Description;
            DueDate = task.DueDate;
            CompletionStatus = task.CompletionStatus;
            UserId = task.UserId;
            TeamMembers = new SelectList(members, "Id", "UserName", new { task.UserId });
            Section = task.Section.Id;
            Sections = new SelectList(sections, "Id", "Name", new { task.Section.Id });
            BoardSection = task.BoardSection.Id;
            BoardSections = new SelectList(boardSections, "Id", "Name", new { task.BoardSection.Id });
        }
        public TaskEditViewModel(TaskEditViewModel viewModel)
        {
            Name = viewModel.Name;
            Description = viewModel.Description;
            DueDate = viewModel.DueDate;
            CompletionStatus = viewModel.CompletionStatus;
            UserId = viewModel.UserId;

        }

        public TaskEditViewModel()
        {

        }

    }
}
