using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartOdin.Core.Database;
using StartOdin.Domain.Entities;
using StartOdin.Domain.Entities.Users;

namespace StartOdin.Controllers;

public class AdminController : Controller
{
    #region Index
    
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }
    
    #endregion
    
    #region Participants 
    
    [Authorize(Roles = "Admin")]
    public IActionResult Participants(int page)
    {
        return View();
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult Participant(int id)
    {
        if (id == 0)
        {
            return RedirectToAction("Index");
        }
        
        var participant= DatabaseController.GetInstance().Participants.FirstOrDefault(x => x.Id == id);

        if (participant == null)
        {
            return RedirectToAction("Index");
        }
        
        return View(participant);
        
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Participant(Participant participant)
    {
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> DeleteParticipant(int id)
    {
        if (id == 0)
        {
            return RedirectToAction("Index");
        }
        
        DatabaseController.GetInstance().Participants.Remove(DatabaseController.GetInstance().Participants.FirstOrDefault(x => x.Id == id));
        await DatabaseController.GetInstance().SaveChangesAsync();
        
        return RedirectToAction("Participants");
    }
    
    #endregion
    
    #region Teams
    
    [Authorize(Roles = "Admin")]
    public IActionResult Teams()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Team(Team team)
    {
        if (team != null)
        {
            var team1 = await DatabaseController.GetInstance().Teams.FirstOrDefaultAsync(x => x.Id == team.Id);

            team1.Name = team.Name;
            team1.Description = team.Description;
            team1.Discipline = team.Discipline;
            team1.GoldMedals = team.GoldMedals;
            team1.SilverMedals = team.SilverMedals;
            team1.BronzeMedals = team.BronzeMedals;
            team1.MembersCount = team.MembersCount;

            await DatabaseController.GetInstance().SaveChangesAsync();
            return RedirectToAction("Teams");
        }
        return RedirectToAction("Teams");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Team(int id)
    {
        if (id < 0)
        {
            return RedirectToAction("Teams");
        }

        var team = await DatabaseController.GetInstance().Teams.FirstOrDefaultAsync(x => x.Id == id);

        if (team == null)
        {
            return RedirectToAction("Index");
        }
        
        return View(team);
    }

    public async Task<IActionResult> DeleteTeam(int id)
    {
        DatabaseController.GetInstance().Teams
            .Remove(DatabaseController.GetInstance().Teams.FirstOrDefault(x => x.Id == id));

        DatabaseController.GetInstance().SaveChangesAsync();
        return RedirectToAction("Teams");
    }
    
    #endregion
    
    #region Games

    [Authorize(Roles = "Admin")]
    public IActionResult Games()
    {
        return View();
    }
    
    #endregion
}