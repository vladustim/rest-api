---
name: create-skill
description: "Use when: packaging a repeatable workflow, debugging method, review checklist, or implementation pattern into a reusable SKILL.md for this project or for personal use."
---

# Create a Reusable Skill

## Goal

Turn a recurring workflow into a reusable skill that another agent or user can invoke to follow the same process consistently.

## When to Use This Skill

Use this skill when:
- the work follows a clear multi-step process
- a debugging, review, implementation, or validation workflow is repeating
- a team or user wants to preserve a proven approach without re-explaining it each time
- a task is too large for a single prompt but not broad enough to require a custom agent

## Decision Flow

1. Identify the workflow being followed.
   - Look for steps, tools, checks, and recurring decisions.
   - Separate the process from project-specific details.

2. Extract the reusable structure.
   - Write the step-by-step flow.
   - Record branching logic such as: "if root cause is unclear, gather evidence first" or "if tests fail, inspect the failing path before changing code".
   - Capture quality gates or completion checks.

3. Decide scope.
   - Workspace-scoped: place the skill under `.github/skills/<name>/SKILL.md` when the workflow is shared with the project.
   - User-scoped: place it in the personal prompt directory when it is for one person's repeatable workflow across projects.

4. Choose the right customization primitive.
   - Use a skill for a multi-step workflow with repeatable actions and bundled assets.
   - Use a prompt for a single focused task with parameters.
   - Use an instruction for broadly shared project rules.
   - Use a custom agent only when isolation or tool restriction is required.

5. Draft the skill file.
   - Add valid YAML frontmatter with a short `name` and a descriptive `description`.
   - Keep the body focused on:
     - goal
     - when to use it
     - step-by-step process
     - decision points
     - quality checks
     - example prompts

6. Validate before finishing.
   - Confirm the file is in the expected location.
   - Check frontmatter is valid YAML.
   - Verify the description contains trigger phrases that match likely requests.
   - Ensure the workflow is clear enough to follow without extra context.

## Completion Checklist

A skill is ready when all of the following are true:
- the task or workflow is clearly defined
- the process is organized into ordered steps
- key branching decisions are described
- completion criteria are explicit
- it is saved in the correct customization location
- it is easy to reuse without project-specific assumptions

## Template

```md
---
name: <skill-name>
description: "Use when: <trigger phrase or scenario>"
---

# <Skill Name>

## Goal

<What this skill produces>

## When to Use This Skill

<Typical situations>

## Process

1. <Step one>
2. <Step two>
3. <Step three>

## Decision Points

- If <condition>, then <action>
- If <condition>, then <action>

## Completion Checklist

- <Verification item>
- <Verification item>
- <Verification item>

## Example Prompts

- "<Example prompt>"
- "<Example prompt>"
```

## Example Prompts

- "Package our debugging workflow into a reusable skill for this repo."
- "Turn our review checklist into a SKILL.md that can be reused by other agents."
- "Create a skill for implementing a new API endpoint with validation and verification."
- "Generalize the workflow we followed into a reusable skill for future tasks."

## Related Customizations to Consider Next

- a project instruction for always-on standards
- a focused prompt for a single repeated task
- a custom agent for isolated multi-stage work
- a hook for enforcing formatting or pre-check validation
