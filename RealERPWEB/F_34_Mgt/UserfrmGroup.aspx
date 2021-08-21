<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="UserfrmGroup.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.UserfrmGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        $(document).on('click', '.panel-heading span.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('fa fa-minus').addClass('fa fa-plus');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('fa fa-plus').addClass('fa fa-minus');
            }
        })

        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            

            var gv = $('#<%=this.gvPermission.ClientID %>');
            gv.Scrollable();
            var gvUseForm = $('#<%=this.gvUseForm.ClientID %>');
            gvUseForm.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

    <style>
        .Profile {
  position: relative;
}

.image {
  opacity: 1;
  display: block;
  transition: .5s ease;
  backface-visibility: hidden;
}

.middle {
  transition: .5s ease;
  opacity: 0;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  text-align: center;
}

.Profile:hover .image {
  opacity: 0.3;
}

.Profile:hover .middle {
  opacity: 1;
}

        .text {
            background-color: #bf6295;
            color: white;
            font-size: 16px;
            padding: 10px 10px;
            color: white;
        }
  .text a {
 
  color:white;
}
   .panel {
    margin-bottom: 20px;
    background-color: #fff;
    border: 1px solid transparent;
    border-radius: 4px;
    -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.05);
    box-shadow: 0 1px 1px rgba(0,0,0,.05);
}
.panel-default {
    border-color: #ddd;
}
.panel-default>.panel-heading {
    color: #333;
    background-color: #f5f5f5;
    border-color: #ddd;
}
.panel-heading {
    padding: 10px 15px;
    border-bottom: 1px solid transparent;
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
}

.panel-body {
    padding: 15px;
}
.tbMenuWrp table tr td {
            /*height: 50px;*/
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            
            cursor: pointer;
            background: #fff;
            position: relative;
        }
 .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }
   .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }
   .tbMenuWrp table tr td label .lblactive {
            background: #667DE8;
            color: #000000;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }
 .circle-tile {
          
            text-align: center;
            width: 180px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 18px;
            height: 36px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 36px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 2px;
            border-radius: 0px 15px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: uppercase;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }
        .dark-blue {
            background-color: #34495E;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
        }
    </style>
    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

          <div class="card card-fluid" style="min-height:550px;">
                <div class="card-body">
                    <br />
                    <div class="row">
                             <div class="col-md-2">
                           <div class="form-group">
                                   
                                        <asp:TextBox ID="txtSrcName" runat="server" CssClass="form-control"></asp:TextBox>
                               </div>
                              </div>
                         <div class="col-md-1">
                                 <div class="form-group">
                                            <asp:LinkButton ID="ibtnFindName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindName_Click" TabIndex="9"><span class="fa fa-search"> </span></asp:LinkButton>
                                </div>
                             </div>
                                  <div class="col-md-1">
                                 <div class="form-group">
                                         <asp:LinkButton ID="lbtnNewUser" OnClick="lbtnNewUser_Click" CssClass="btn btn-danger" runat="server"><span class="glyphicon glyphicon-user "> </span> New User</asp:LinkButton>
                                        

                                    </div>
                                      </div>
                         <div class="col-md-1">
                                    <div class="form-group">

                                        <asp:Label ID="lblId" CssClass="control-label" runat="server" Visible="False" Text="User Name"></asp:Label>
                                   
                                    </div>
                             </div>
                          <div class="col-md-1">
                                    <div class="form-group">
                                             <asp:Label ID="txtuserid" CssClass="" runat="server" Visible="False" Text="User Name"></asp:Label>

                                    </div>
                              </div>
                                    <div class="col-md-3 pading5px asitCol3 pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblMsg" CssClass="btn-danger primaryBtn btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            
                        
                   
                    <div class="row">
                        <div class="col-md-9">                
                              <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="918px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvUseForm_PageIndexChanging"               
                            PageSize="100">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="EditBtn" OnClick="EditBtn_Click" runat="server" Text="<span class='fa fa-edit'></span>"></asp:LinkButton>
                                      </ItemTemplate>
                                      </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUserId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpusrid")) %>'
                                            Width="50px" OnClick="lbtnUserId_Click"></asp:LinkButton>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvuserid" runat="server" BackColor="Transparent"
                                            BorderStyle="None" MaxLength="7" Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpusrid")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="lgvusrShorName_Click" ID="lgvusrShorName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                            Width="120px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusrFullName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrFullName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                    </EditItemTemplate>


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pass Word" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="140px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                          <%--    <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                    </ItemTemplate>
                                   <EditItemTemplate>
                                                    
                                                </EditItemTemplate>
                               <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp ID" >
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel21" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSrCentrid" runat="server" CssClass="  inputtextbox" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td>

                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="ibtnSrchCentr" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnSrchCentr_Click" TabIndex="14"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </td>
                                                    <td>
                                                        <div class="col-md-4 pading5px">
                                                            <asp:DropDownList ID="ddlempid" runat="server" CssClass="form-control inputTxt chzn-select" Width="200px">
                                                            </asp:DropDownList>
                                                        </div>

                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCentrName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Emp" Visible="false">
                                    

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True"  HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <PagerSettings Position="Top" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView></div>
                        <div class="col-md-3" id="userDetPan" runat="server" visible="false">
                            <br />
                        
                            <asp:Label ID="lbluserheading" runat="server" Font-Bold="true" Style="margin-bottom:0px;" class="alert alert-danger" Width="100%"></asp:Label>
                                
                            <asp:GridView ID="indUsrinf" runat="server" AutoGenerateColumns="False" Style="margin-top:0px;"
                           CssClass="table table-striped table-hover table-bordered grvContentarea">
                                   <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnComname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtncomusrid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                       </Columns>

                            </asp:GridView>
                        </div>
      
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                            <div class="col-md-3">
                                 <div class="form-group">                                      
                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control inputTxt">
                                   </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="d-flex justify-content-between" Text="Page Size" Visible="false"></asp:Label>
                                     <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control"
                                                    BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                   >
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                    <asp:ListItem Value="400">400</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="d-flex justify-content-between" Text="Select Module" ></asp:Label>

                                    <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass="form-control inputTxt">
                                   </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                        <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True"
                                                    Font-Bold="True"
                                                    OnCheckedChanged="chkShowall_CheckedChanged" Text="Show All" CssClass="btn btn-primary checkBox" />
                                              <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn  btn-warning primaryBtn pull-right"
                                                    OnClick="lnkbtnBack_Click">Back</asp:LinkButton>
                                       
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblMsg1" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                
                                </div>
                            </div>
                                <div class="col-md-9">
                                       <asp:GridView ID="gvPermission" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPermission_PageIndexChanging"
                                    OnRowDeleting="gvPermission_RowDeleting" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvufrmname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvufrmid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQrytype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDescription" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "dscrption").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "dscrption")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="280px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpPer_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style22">Description</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td>
                                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Permission">
                                            <HeaderTemplate>
                                                <table style="width: 90px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Permission"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallView_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPermit" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry or Edit">
                                            <HeaderTemplate>
                                                <table style="width: 90px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Entry or Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallEntry_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEntry" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entry"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                                <HeaderTemplate>
                                                    <table style="width: 90px;">
                                                       
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Labsel5" runat="server" Text="Delete"></asp:Label>
                                                                <asp:CheckBox ID="chkAllDel" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllDel_CheckedChanged"
                                                                     />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                   <asp:CheckBox ID="delete" runat="server" AutoPostBack="True" 
                                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delete"))=="True" %>'
                                                                     />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print">
                                            <HeaderTemplate>
                                                <table style="width: 90px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Print"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallPrint_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPrint" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Row All">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkall_CheckedChanged" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerSettings Position="Top" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                </asp:GridView>
                                <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>
            
                                </div>
                               
                                             </div>
                        </asp:View>
                        <asp:View ID="NewUser" runat="server">
                  
                     <div class="row">
                       
                         <div class="col-md-5">
                             <div id="labels" class="card">
                  <!-- .card-body -->
                  <div class="card-body">
                    <!-- .form -->
                 
                      <!-- .fieldset -->
                      <fieldset>
                        <legend>New User Registration
                                   <asp:LinkButton ID="btnClose" runat="server" CssClass="btn btn-xs btn-warning " OnClick="btnClose_Click">Back</asp:LinkButton>
                          

                        </legend> <!-- .form-group -->
                        <div class="form-group">
                          <label for="lbl1">User Name <abbr title="Required User Name">*</abbr></label> 
                             <asp:TextBox ID="txtUsr" runat="server" CssClass="form-control" placeholder="User Name" required=""></asp:TextBox>
                            <%--<input type="text" class="form-control" id="lbl1" placeholder="Required asterisks" required="">--%>
                        </div><!-- /.form-group -->
                        <!-- .form-group -->
                        <div class="form-group">
                          <label for="lbl2">Full Name<span class="badge badge-danger">Required</span></label> 
                             <asp:Label ID="LblUpFlag" runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox ID="TxtFullName" runat="server" CssClass="form-control" required=""></asp:TextBox>
                            
                        </div><!-- /.form-group -->
                        <!-- .form-group -->
                        <div class="form-group">
                          <label for="lbl3">Designation <span class="badge badge-secondary"><em>Optional</em></span></label>
                       <asp:TextBox ID="TxtDesg" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                        </div><!-- /.form-group -->
                            <div class="form-group">
                          <label class="d-flex justify-content-between" for="lbl5">Password <abbr title="Note">(Fill before Click update button)</abbr>
                              <a href="#TxtPass" data-toggle="password"><i class="fa fa-fw fa-eye"></i> <span>Show</span></a></label> 
                                 <asp:TextBox ID="TxtPass"  ClientIDMode="Static"  runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                     <asp:Label ID="Grpusr" runat="server" Visible="false"></asp:Label>
                                
                        </div><!-- /.form-group -->
                             <!-- .form-group -->
                        <div class="form-group">
                          <label for="lbl2">Email</label> 
                             
                                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" type="email"></asp:TextBox>
                            
                        </div><!-- /.form-group -->
                        <!-- .form-group -->
                        <div class="form-group">
                          <label class="d-flex justify-content-between" for="lbl4"><span>Remarks</span> <span class="text-muted">0 of 80 characters used</span></label>
                           <asp:TextBox ID="TxtRemark" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                           
                        </div><!-- /.form-group -->
                        <!-- .form-group -->

                           <div class="form-group">  
                       <label class="d-flex justify-content-between">User Role</label> 
                       <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="chzn-select form-control" >                                        
                                    </asp:DropDownList>
                    </div> 
                          
                           <div class="form-group">  
                       <label class="d-flex justify-content-between">HR Employee</label> 
                      <asp:DropDownList ID="ddlHrEmp" runat="server" CssClass="chzn-select form-control">                                        
                                    </asp:DropDownList>
                    </div> 
                          
                        

                     
                        

               
                      </fieldset><!-- /.fieldset -->
                 
                  </div><!-- /.card-body -->
                </div>





                         </div>
                        <div class="col-md-7">
                            <!--- start company and profile information----->
                              <div class="panel panel-default">
    				<div class="panel-heading">
    					<h4 class="panel-title"><span id="ProfilePenlTitle" runat="server" class="clickable small "><i class="fa fa-minus "></i></span> Company Information and Profile
    					
                            </h4>
    				</div>
    				<div class="panel-body" id="ProfilePenl" runat="server">
                        <div class="row">

                      
                         <div class="col-md-7">
                            <div class="row">
                                  <div class="col-md-8">
                                <div class="form-group">  
                       <label class="d-flex justify-content-between">Company</label> 
                       <asp:DropDownList ID="ddlComp" runat="server" CssClass="chzn-select form-control" >
                                        </asp:DropDownList>
                              </div> 
                                       <div class="form-group">
                          <label>Status of This user?</label>
                          <div class="custom-control custom-checkbox">
                              <input type="checkbox" id="actChkbox" runat="server" class="custom-control-input" checked="checked" >
                              <%--<asp:CheckBox ID="actChkbox" class="custom-control-input" runat="server" Checked="true" />--%>
                            <label class="custom-control-label" for="ckb7">Yes, This user active instant after Save</label>
                          </div>
                        </div>
                            </div>
                                <div class="col-md-4">
                              <div class="form-group">
                                  <asp:Button ID="btnAdd" style="margin-top:30px;" runat="server" CssClass=" btn btn-xs btn-primary" OnClick="btnAdd_Click" Text="Add" />
                                
                          </div>
                                    </div>
                            </div>
                          
                             <asp:GridView ID="gvcompany" runat="server" AutoGenerateColumns="False"    OnRowDeleting="gvcompany_RowDeleting"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='fa fa-trash-alt'></span>" />
                          



                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpcomp" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>' Width="210px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            <%--    <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusr" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>' Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="User" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusrShort" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Full Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsrFullName" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesg" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Active" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActiveSt" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                    </ItemTemplate>
                                   <EditItemTemplate>
                                                    
                                                </EditItemTemplate>
                               <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>

                            <div class="form-group">
                                      
                                         <asp:Button ID="lbtnUpdate" CssClass="btn btn-xs btn-danger" OnClick="lbtnUpdate_Click" runat="server" Text="Update" />
                            
                            </div>
                        </div>
                         <div class="col-md-5">

                             <div class="Profile">
  <asp:Image ID="UserImage" CssClass="img img-thumbnail image" runat="server" />
  <div class="middle">
    <div class="text"> <a href="#" class="" data-toggle="modal" data-target="#followingModal">Change Profile</a></div>
  </div>
</div>



                            
                            
                               <!-- .modal -->
            <div class="modal fade" id="followingModal" tabindex="-1" role="dialog" aria-labelledby="followingModalLabel" aria-hidden="true">
              <!-- .modal-dialog -->
              <div class="modal-dialog modal-dialog-scrollable" role="document">
                <!-- .modal-content -->
                <div class="modal-content">
                  <!-- .modal-header -->
                  <div class="modal-header">
                    <h6 id="followingModalLabel" class="modal-title"><span class="fa fa-user-tag"></span> Change Your Profile Picture </h6>
                  </div><!-- /.modal-header -->
                  <!-- .modal-body -->
                  <div class="modal-body px-0">
                 
                  <div class="card-body">                      
                        <div id="dropzone" class="fileinput-dropzone">
                          <span>Drop files or click to upload.</span> <!-- The file input field used as target for the file upload widget -->
                          <asp:FileUpload ID="fileuploaddropzone" runat="server" 
                                                    onchange="submitform();"  />
                          
                        </div>
                      <div id="progress" class="progress progress-xs rounded-0 fade">
                        <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                      </div>
                      
                  
                      </div>
                     
                 
                   
                  </div><!-- /.modal-body -->
                  <!-- .modal-footer -->
                  <div class="modal-footer">
                     
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                  </div><!-- /.modal-footer -->
                </div><!-- /.modal-content -->
              </div><!-- /.modal-dialog -->
            </div><!-- /.modal -->
                         </div>

                              </div>   
                     </div>
                         
                                  </div>
                            <!------------------start menu permission.....--------->
                            <div class="panel panel-default">
    				<div class="panel-heading">
    					<h4 class="panel-title"><span id="PermissionPanel" runat="server" class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span> Menu Permission  					
                            </h4>
    				</div>
    				<div class="panel-body" id="PermissionPnl" runat="server" style="display: none;">
                             <div class="col-md-12">
                             <div class="form-group">
                                  <asp:DropDownList ID="ddlPermitedComp" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlPermitedComp_SelectedIndexChanged" CssClass="form-control inputTxt">
                                   </asp:DropDownList>
                                  
                             </div>
                             </div>

                                                  <div class="tbMenuWrp nav nav-tabs">
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True" Value="0"><div class='circle-tile'><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Menu Permission</div></div></div></asp:ListItem>
                                                                <asp:ListItem Value="1"><div class='circle-tile'><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Cash/Bank Permission</div></div></div></asp:ListItem>
                                                                <asp:ListItem Value="2"><div class='circle-tile'><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> Project Permission</div></div></div></asp:ListItem>
                                                               
                                                            </asp:RadioButtonList>
                                                        </div>
                        <!------------this panel use for Menu permission-------------------------->
                        <asp:Panel ID="PanelMenu" runat="server" Visible="true">
                            <div class="row">                     
                        <div class="col-md-8">
                        <div class="form-group">  
                        <asp:DropDownList ID="ddlConTrolCode" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                         </div>
                        </div>
                        <div class="col-md-4">
                             <div class="form-group">  
                                  
                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                   
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server"  CssClass="btn btn-primary  " OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                     
                          
                             </div>
                            </div>
                           </div>
                         <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                                OnRowDeleting="gvProLinkInfo_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='fa fa-trash-alt'></span>" />

                                    <asp:TemplateField HeaderText="Module Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "moduleid")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module Name">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                      
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                Width="150px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                          <asp:LinkButton ID="LbtnMenuUpdate" runat="server" Font-Bold="True"
                                                Font-Size="13px" OnClick="LbtnMenuUpdate_Click"
                                               CssClass="btn btn-sm btn-success">Final Update</asp:LinkButton>
                        </asp:Panel>

                        <!----------------------this panel use for Cash Bank Permission--------->
                        <asp:Panel ID="PanelCashbank" runat="server" Visible="false">

                               <div class="row">                     
                        <div class="col-md-8">
                        <div class="form-group">  
                        <asp:DropDownList ID="ddlCashControlCode" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                         </div>
                        </div>
                        <div class="col-md-4">
                             <div class="form-group">  
                                  
                                        <asp:LinkButton ID="lbtnCashBnkselect" runat="server" CssClass="btn btn-primary" OnClick="lbtnCashBnkselect_Click">Select</asp:LinkButton>
                                   
                                        <asp:LinkButton ID="lbtnCashBnkselectall" runat="server"  CssClass="btn btn-primary  " OnClick="lbtnCashBnkselectall_Click">Select All</asp:LinkButton>

                     
                          
                             </div>
                            </div>
                           </div>


                            <asp:GridView ID="gvCashbank" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                                OnRowDeleting="gvCashbank_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='fa fa-trash-alt'></span>"  />

                                    <asp:TemplateField HeaderText="bactcode Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                Style="text-align: center;" Width="90px">Delete All</asp:LinkButton>
                                        </FooterTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                     
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                Width="150px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                              <asp:LinkButton ID="LbtnCashBankUpdate" runat="server" CssClass="btn btn-sm btn-primary" OnClick="LbtnCashBankUpdate_Click"
                                               >Final Update</asp:LinkButton>
                               
                        </asp:Panel>
                          <asp:Panel ID="Panelroject" runat="server" Visible="false">
                                 <div class="row">                     
                        <div class="col-md-8">
                        <div class="form-group">  
                         <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                         </div>
                        </div>
                        <div class="col-md-4">
                             <div class="form-group">  
                                  
                                        <asp:LinkButton ID="lbtnSelectProject" runat="server" CssClass="btn btn-primary" OnClick="lbtnSelectProject_Click">Select</asp:LinkButton>
                                   
                                        <asp:LinkButton ID="lbtnSelectAllProject" runat="server"  CssClass="btn btn-primary  " OnClick="lbtnSelectAllProject_Click">Select All</asp:LinkButton>

                     
                          
                             </div>
                            </div>
                           </div>

                                <asp:GridView ID="gvProject" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                                OnRowDeleting="gvProject_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="user Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod0" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:CommandField ShowDeleteButton="True" />

                                    <asp:TemplateField HeaderText="pactcode Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprocode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                   
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                Width="150px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                                   <asp:LinkButton ID="lbtnSuplUpdate" runat="server" OnClick="lbtnSuplUpdate_Click"
                                                CssClass="btn  btn-danger btn-sm">Final Update</asp:LinkButton>
                               
                        </asp:Panel>
                        
                              
                        </div>
                        </div>
                        </div>
                    

                     </div>

                                                 
                      
                   
                           
                        </asp:View>
                    </asp:MultiView>

                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

