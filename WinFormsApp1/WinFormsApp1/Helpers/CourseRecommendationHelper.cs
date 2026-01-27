using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service;
using WinFormsApp1.View.User.Controls;

namespace WinFormsApp1.Helpers
{
    public static class CourseRecommendationHelper
    {
        public static CourseCardControl CreateRecommendedCourseCard(RecommendedCourse recommended)
        {
            var card = new CourseCardControl();
            card.Bind(recommended.Course);

            if (recommended.ReasonTags != null && recommended.ReasonTags.Any())
            {
                var pnlCard = card.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == "pnlCard");
                if (pnlCard != null)
                {
                    var pnlTagsOverlay = pnlCard.Controls.OfType<FlowLayoutPanel>()
                        .FirstOrDefault(p => p.Name == "pnlTagsOverlay");
                    
                    if (pnlTagsOverlay != null)
                    {
                        pnlTagsOverlay.Controls.Clear();
                        pnlTagsOverlay.Visible = true;

                        foreach (var reason in recommended.ReasonTags.Take(2))
                        {
                            var tag = new Label
                            {
                                Text = reason,
                                Font = new Font("Segoe UI", 7.5F),
                                ForeColor = Color.White,
                                BackColor = Color.FromArgb(124, 77, 255),
                                AutoSize = true,
                                Padding = new Padding(6, 3, 6, 3),
                                Margin = new Padding(3, 0, 3, 0)
                            };
                            pnlTagsOverlay.Controls.Add(tag);
                        }
                    }
                }
            }
            else
            {
                // Ẩn panel tags nếu không có tags
                var pnlCard = card.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == "pnlCard");
                if (pnlCard != null)
                {
                    var pnlTagsOverlay = pnlCard.Controls.OfType<FlowLayoutPanel>()
                        .FirstOrDefault(p => p.Name == "pnlTagsOverlay");
                    
                    if (pnlTagsOverlay != null)
                    {
                        pnlTagsOverlay.Visible = false;
                    }
                }
            }

            return card;
        }
    }
}
