$('#newSkillText').autocomplete({ source: model.CanonicalSkillNames });

var ViewModel = function() {
    var self = this;
    self.skills = ko.observableArray(model.ConsultantSkills);
    self.sortedSkills = ko.computed(function() {
        var list = [];
        for (var i = 0; i < self.skills().length; i++) {
            list.push(self.skills()[i]);
        }
        list = _.sortBy(list, function(x) { 
            return x.SkillName; 
        });
        list = _.unique(list, function(x) { return x.SkillId; });
        return list;
    });
    self.addSkill = function() {
        var skillName = $('#newSkillText').val();
        var proficiencyLevel = $('#newSkillProficiencyLevel').val();
        $.ajax({
            type: 'POST',
            data: { consultantId: model.ConsultantId, skillName: skillName, proficiencyLevel: proficiencyLevel },
            url: model.addSkillUrl,
            success: function(skill) {
                if (skill) {
                    self.skills.push(skill);
                }
            },
            error: function() {
                alert('Unable to add ' + skillName);
            }
        });
    };
    self.removeSkill = function(skill) {
        if (confirm('Are you sure you want to remove ' + skill.SkillName + '?')) {
            $.ajax({
                type: 'POST',
                data: { consultantId: model.ConsultantId, skillId: skill.SkillId },
                url: model.removeSkillUrl,
                success: function() {
                    self.skills.remove(skill);
                },
                error: function() {
                    alert('Unable to remove ' + skill.SkillName);
                }
            });
        }
    }
    return this;
};
ko.applyBindings(new ViewModel());